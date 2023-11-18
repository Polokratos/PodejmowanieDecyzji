import { useReducer, useState } from "react"
import { SurveyBodyComponent } from "./SurveyBodyComponent";
import { Answer, Question, Survey } from "../../types/types";
import { answerReducer } from "./reducer/AnswerReducer";



export const SurveyComponent = (props:{survey:Survey}) : JSX.Element => {

    const {survey} = props;

    const createQuestionHandler = (valuesSeed:Question[]) => {
        const [isOpen,setOpen] = useState(false);

        const [values,setValues] = useState(valuesSeed);
        const [iterator,setIterator] = useState(0);

        const move = (by: number) => setIterator(c => (c+by+valuesSeed.length)%valuesSeed.length);
        const currentQuestion = values[iterator];
        const setAnswer = (ans:Answer) => {
            const newState = [...values];
            newState[iterator] = {...newState[iterator], answer:ans};
            setValues(newState);
        }
        return {
            questions : values,
            currentQuestion,
            isOpen,
            setOpen,
            toggleOpen : () => setOpen(open=>!open),
            answer : currentQuestion.answer ?? "",
            setAnswer,
            previous : () => move(-1),
            next : () => move(1),
        }
    };
    
    const alternativesHandler = createQuestionHandler(survey.alternatives);
    const criteriaHandler = createQuestionHandler(survey.criteria);

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
                Survey Name: "{survey?.name}"
            </div>
            <button className="drawer-header-button" onClick={criteriaHandler.toggleOpen}>
                {criteriaHandler.isOpen ? 'Close ' : 'Open '}Criteria
            </button>
            <button className="drawer-header-button" onClick={alternativesHandler.toggleOpen}>
                {alternativesHandler.isOpen ? 'Close ' : 'Open '}Alternatives
            </button>
            <button className="drawer-header-button" onClick={submit}>
                Submit and send
            </button>
        </div>
    );

    return ( 
    <div className="drawer">
        {Header}   
        {alternativesHandler.isOpen && <SurveyBodyComponent surveyContext={survey.context} {...alternativesHandler} />}
        {alternativesHandler.isOpen && criteriaHandler.isOpen && <div className="drawer-delimiter"></div>}
        {criteriaHandler.isOpen && <SurveyBodyComponent surveyContext={survey.context} {...alternativesHandler} />}
    </div>
    );
}