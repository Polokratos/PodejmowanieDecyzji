import { useEffect, useReducer, useRef, useState } from "react"
import { SurveyBodyComponent } from "./SurveyBodyComponent";
import { Answer, Survey, SurveyField } from "../../types/types";
import { answerReducer } from "./reducer/AnswerReducer";



export const SurveyComponent = (props:{survey:Survey}) : JSX.Element => {

    const {survey} = props;

    const [alternatives,dispatchALT] = useReducer(answerReducer,survey.alternatives);
    const [criteria, dispatchCRI] = useReducer(answerReducer,survey.criteria);


    const createContentHandler = <T,>(values:T[]) => {
        const [isOpen,setOpen] = useState(false);
        const [iterator,setIterator] = useState(0);
        const move = (by:number) => {setIterator((iterator+by+values.length)%values.length);};
        return {
            activeIndex : iterator,
            isOpen,
            setOpen,
            toggleOpen : () => setOpen((o) => !o),
            move,
        }
    };
    
    const alternativesHandler = createContentHandler(survey.alternatives);
    const criteriaHandler = createContentHandler(survey.criteria);

    const submit = () => {
        //debug logging
        console.log(alternatives);
        console.log(criteria);
        
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
        {alternativesHandler.isOpen && <SurveyBodyComponent 
            question={alternatives[alternativesHandler.activeIndex]} 
            surveyContext={survey.context} 
            onNext={() => {
                alternativesHandler.move(1);
            }} 
            onPrev={() => {
                alternativesHandler.move(-1);
            }} 
            setAnswer={ans => {
                dispatchALT({answer:ans, id:alternatives[alternativesHandler.activeIndex].id});
            }}
            answer={alternatives[alternativesHandler.activeIndex].answer ?? ""} />}
        {alternativesHandler.isOpen && criteriaHandler.isOpen && <div className="drawer-delimiter"></div>}
        {criteriaHandler.isOpen && <SurveyBodyComponent
            question={criteria[criteriaHandler.activeIndex]} 
            surveyContext={survey.context}
            onNext={() => {
                criteriaHandler.move(1);
            }} 
            onPrev={() => {
                criteriaHandler.move(-1);
            }}
            setAnswer={ans => {
                dispatchCRI({answer:ans, id:criteria[criteriaHandler.activeIndex].id});
            }} 
            answer={criteria[criteriaHandler.activeIndex].answer ?? ""}/>}
    </div>
    );
}