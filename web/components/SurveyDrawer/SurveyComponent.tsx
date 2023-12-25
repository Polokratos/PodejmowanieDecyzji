import { useEffect, useState } from "react"
import { SurveyBodyComponent } from "./SurveyBodyComponent";
import { Answer, Question, SurveyField, SurveyHeader, TestSurveyDetails } from "../../types/types";
import { AlternativeDTO, CriterionDTO, RankingAnswerDTO, RankingPostDTO, fetchService } from "../../services/fetchService";



export const SurveyComponent = (props:SurveyHeader) : JSX.Element => {

    const {id,name} = props;
    const [context,setContext] = useState<SurveyField>("");

    const createQuestionHandler = () => {
        const [isOpen,setOpen] = useState(false);
        const toggleOpen = () => setOpen(!isOpen);
        const [questions,setQuestions] = useState<Question[]>([]);
        const [iterator,setIterator] = useState(0);

        const move = (by: number) => setIterator(c => (c+by+questions.length)%questions.length);
        const currentQuestion = questions[iterator];
        const setAnswer = (ans:Answer) => {
            const newState = [...questions];
            newState[iterator] = {...newState[iterator], answer:ans};
            setQuestions(newState);
        }
        return {
            questions, setQuestions, currentQuestion,
            isOpen, setOpen, toggleOpen,
            answer : currentQuestion?.answer ?? 0, setAnswer,
            previous : () => move(-1),
            next : () => move(1),
            isActive : questions.length > 0
        }
    };
    
    const alternativesHandler = createQuestionHandler();
    const criteriaHandler = createQuestionHandler();

    //mock api call
    useEffect(() =>{
        const rs = fetchService.getSurvey(id);
        rs.then(dto => {
            setContext(dto.description ?? "");
            const alts = dto.alternatives ?? [];
            const crits = dto.criteria ?? [];
            const questions = crits
                .map(c => [...alts].map(a1 => [...alts].map((a2) : Question => (
                    {
                        id : c.criterionId,
                        id1 : a1.alternativeId,
                        option1 : a1.description,
                        id2 : a2.alternativeId,
                        option2: a2.description
                    }))))
                .flat(2)
                // shuffle
                .map(v => ({v, key: Math.random()}))
                .sort((a,b) => (a.key - b.key)) 
                .map(v => v.v);
            alternativesHandler.setQuestions(questions);
        })
    },[]);


    const submit = () => {
        const toDTO = (q:Question) : RankingAnswerDTO => {
            return {
                criterionID : q.id,
                leftAlternativeID : q.id1,
                rightAlternativeID : q.id2,
                value: q.answer ?? NaN
            }
        }
        const alternativesAnswers : RankingAnswerDTO[] = alternativesHandler.questions.map(toDTO)
        const criteriaAnswers : RankingAnswerDTO[] = [] //criteriaHandler.questions.map(toDTO) criteria are broken
        const body : RankingPostDTO = {
            rankingID : id,
            answers : [...alternativesAnswers,...criteriaAnswers]
        }
        fetchService.submitAnswer(body);
        console.log(alternativesHandler.questions.map(e=>e.answer));
        console.log(criteriaHandler.questions.map(e=>e.answer));
        
        alternativesHandler.setOpen(false);
        criteriaHandler.setOpen(false);
    }
    const Header = (
        <div className="drawer-header">
            <div className="drawer-header-content">
                Survey Name: "{name}"
            </div>
            { criteriaHandler.isActive && 
                <button className="drawer-header-button" onClick={criteriaHandler.toggleOpen}>
                    {criteriaHandler.isOpen ? 'Close ' : 'Open '}Criteria
                </button>}
            { alternativesHandler.isActive &&
                <button className="drawer-header-button" onClick={alternativesHandler.toggleOpen}>
                    {alternativesHandler.isOpen ? 'Close ' : 'Open '}Alternatives
                </button>}
            { (criteriaHandler.isActive || alternativesHandler.isActive) &&
                <button className="drawer-header-button" onClick={submit}>
                    Submit and send
                </button>}
        </div>
    );

    return ( 
    <div className="drawer">
        {Header}
        {alternativesHandler.isOpen && <SurveyBodyComponent surveyContext={context} {...alternativesHandler} />}
        {alternativesHandler.isOpen && criteriaHandler.isOpen && <div className="drawer-delimiter"></div>}
        {criteriaHandler.isOpen && <SurveyBodyComponent surveyContext={context} {...criteriaHandler} />}
    </div>
    );
}