import { useEffect, useState } from "react"
import { SurveyBodyComponent } from "./SurveyBodyComponent";
import { Answer, Question, SurveyField, SurveyHeader, TestSurveyDetails } from "../../types/types";



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
            answer : currentQuestion?.answer ?? "", setAnswer,
            previous : () => move(-1),
            next : () => move(1),
            isActive : questions.length > 0
        }
    };
    
    const alternativesHandler = createQuestionHandler();
    const criteriaHandler = createQuestionHandler();

    //mock api call
    useEffect(() =>{
        setTimeout(() => setContext(TestSurveyDetails.context),200);
        setTimeout(() => alternativesHandler.setQuestions(TestSurveyDetails.alternatives),300);
        setTimeout(() => criteriaHandler.setQuestions(TestSurveyDetails.criteria),500);
    },[]);

    const submit = () => {
        //debug logging
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