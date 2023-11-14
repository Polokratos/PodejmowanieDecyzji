//user.html
import { useRouter } from "next/router";
import { useState } from "react";
import { SurveyComponent } from "../components/drawer/SurveyComponent";
import { TestSurvey } from "../types/types";


const UserPage = () => {
    
    const getPayload = () => {
        const router = useRouter();
        return router.query.payload;
    }

    return (
    <div>
        <p>Hello, {getPayload()}</p>
        <SurveyComponent surveySeed={TestSurvey}/>
        <SurveyComponent surveySeed={TestSurvey}/>
    </div>
    );
}

export default UserPage;