//user.html
import { useRouter } from "next/router";
import { useState } from "react";
import { SurveyComponent } from "../components/drawer/SurveyComponent";


const UserPage = () => {
    
    const getPayload = () => {
        const router = useRouter();
        return router.query.payload;
    }

    return (
    <div>
        <p>Hello, {getPayload()}</p>
        <SurveyComponent surveyName="testing survey"/>
        <SurveyComponent surveyName="second test survey"/>
    </div>
    );
}

export default UserPage;