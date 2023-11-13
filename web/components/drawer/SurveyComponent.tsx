import { useState } from "react"
import { SurveyBodyComponent, SurveyBodyComponentProps } from "./SurveyBodyComponent";


class SurveyComponentProps {
    surveyName? : string = "Unnamed Survey";

}

export const SurveyComponent = (props:SurveyComponentProps) : JSX.Element => {
    
    const [isOpenAlternatives,setOpenAlternatives] = useState<boolean>(false);
    const toggleOpenAlternatives = () => setOpenAlternatives(!isOpenAlternatives);

    const [isOpenCriteria,setOpenCriteria] = useState<boolean>(false);
    const toggleOpenCriteria = () => setOpenCriteria(!isOpenCriteria);

    const submit = () => {
        console.log("Submitted");   
    }

    const Header = (
        <div className="drawer-header">
            <div className="drawer-header-content">
                Survey Name: "{props.surveyName}"
            </div>
            <button className="drawer-header-button" onClick={toggleOpenCriteria}>
                {isOpenCriteria ? 'Close ' : 'Open '}Criteria
            </button>
            <button className="drawer-header-button" onClick={toggleOpenAlternatives}>
                {isOpenAlternatives ? 'Close ' : 'Open '}Alternatives
            </button>
            <button className="drawer-header-button" onClick={submit}>
                Submit and send
            </button>
        </div>
    );

    return ( 
    <div className="drawer">
        {Header}   
        {isOpenAlternatives && <SurveyBodyComponent OptionOne="alt 1" OptionTwo="alt2" Context="Alternative context"></SurveyBodyComponent>}
        {isOpenAlternatives && isOpenCriteria && <div className="drawer-delimiter"></div>}
        {isOpenCriteria && <SurveyBodyComponent OptionOne="kryt 1" OptionTwo="kryt 2" Context="Criterium context"></SurveyBodyComponent>}
    </div>
    );
}