import { Question, SurveyField } from "../../types/types"

export class SurveyBodyProps {
    question: Question
    surveyContext : SurveyField
    onPrev : () => void
    onNext : () => void
    onInputChange : (userInput:number) => void
}

export const SurveyBodyComponent = (props : SurveyBodyProps) : JSX.Element => {
    
    const {question,surveyContext,onNext,onPrev} = props;

    return (
        <div className="drawer-body">   
            <div className="drawer-body-row-center">
                <div className="drawer-context">{question.context ?? surveyContext ?? ""}</div>
            </div>

            <div className="drawer-body-row-center">
                <div className="drawer-body-option">{question.option1}</div>
                <div className="drawer-body-option">{question.option2}</div>
            </div>
            
            <div className="drawer-body-row-sb">
                <button className="drawer-body-button" onClick={onPrev}>{"Prev"}</button>
                <div className="drawer-input-container">
                    <a> input context</a>
                    <input value={"Input placeholder"}></input>
                </div>
                <button className="drawer-body-button" onClick={onNext}>{"Next"}</button>
            </div>
        </div>
    )
}