import { useState } from "react"
import { Answer, Question, SurveyField } from "../../types/types"

export class SurveyBodyProps {
    question: Question
    surveyContext : SurveyField
    onPrev : () => void
    onNext : () => void
    answer: Answer
    setAnswer: (answer: Answer) => void
}

export const SurveyBodyComponent = (props : SurveyBodyProps) : JSX.Element => {
    
    const {question,surveyContext,onNext,onPrev,answer,setAnswer} = props;

    const QuestionContainer = (
        <>
            <div className="drawer-body-row-center">
                <div className="drawer-context">{question.context ?? surveyContext ?? ""}</div>
            </div>
            <div className="drawer-body-row-center">
                <div className="drawer-body-option">{question.option1}</div>
                <div className="drawer-body-option">{question.option2}</div>
            </div>
        </>
    );

    const AnswerContainer = (
        <div className="drawer-body-row-sb">
                <button className="drawer-body-button" onClick={()=>{onPrev();}}>{"Prev"}</button>
                <div className="drawer-input-container">
                    <a> input context</a>
                    <input value={answer} onChange={(e) => {setAnswer(e.target.value);}}/>
                </div>
                <button className="drawer-body-button" onClick={() => {onNext();}}>{"Next"}</button>
            </div>
    )


    return (
        <div className="drawer-body">   
            {QuestionContainer}
            {AnswerContainer}
        </div>
    )
}