import { useRef, useState } from "react"
import { SurveyBodyComponent } from "./SurveyBodyComponent";
import { Survey, SurveyField } from "../../types/types";



export const SurveyComponent = (props:{surveySeed:Survey}) : JSX.Element => {
    
    const {current:survey} = useRef(props.surveySeed); //seed via props, do not replace
    
    const createContentHandler = <T,>(values:T[]) => {
        const [isOpen,setOpen] = useState(false);
        const [iterator,setIterator] = useState(0);
        const increment = () => {setIterator((iterator+1)%values.length);};
        const decrement = () => {setIterator((iterator-1+values.length)%values.length)}
        return {
            isOpen,
            setOpen,
            toggleOpen : () => setOpen(!isOpen),
            active : values[iterator],
            increment,
            decrement,
            next : () => {increment(); return values[iterator];},
            prev : () => {decrement(); return values[iterator];},
        }
    };
    
    const alternativesHandler = createContentHandler(survey.alternatives);
    const criteriaHandler = createContentHandler(survey.criteria);

    const submit = () => {
        console.log(survey);   
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
            question={alternativesHandler.active} 
            surveyContext={survey.context} 
            onNext={alternativesHandler.increment} 
            onPrev={alternativesHandler.decrement}
            onInputChange={(v) => alternativesHandler.active.answer = v} />}
        {alternativesHandler.isOpen && criteriaHandler.isOpen && <div className="drawer-delimiter"></div>}
        {criteriaHandler.isOpen && <SurveyBodyComponent
            question={criteriaHandler.active} 
            surveyContext={survey.context}
            onNext={criteriaHandler.increment}
            onPrev={criteriaHandler.decrement}
            onInputChange={(v) => alternativesHandler.active.answer = v} />}
    </div>
    );
}