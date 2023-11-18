//user.html
import { useRouter } from "next/router";
import { useEffect, useState } from "react";
import { SurveyComponent } from "../components/SurveyDrawer/SurveyComponent";
import { SurveyHeader, TestSurveyHeader } from "../types/types";


const UserPage = () => {
    
    const getPayload = () => {
        const router = useRouter();
        return router.query.payload;
    }

    const [surveys,setSurveys] = useState<SurveyHeader[]>([]);

    //mock api call
    useEffect(() => {
        setTimeout(()=>{setSurveys([TestSurveyHeader,TestSurveyHeader])},2000);
    },[]);

    return (
    <div>
        <p>Hello, {getPayload()}</p>
        {surveys.map(s => <SurveyComponent key={s.id + Math.random() /*Random since for mocks we have same ID*/} {...s}/>)}
    </div>
    );
}

export default UserPage;