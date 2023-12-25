//user.html
import { useRouter } from "next/router";
import { useEffect, useState } from "react";
import { SurveyComponent } from "../components/SurveyDrawer/SurveyComponent";
import { SurveyHeader, TestSurveyHeader } from "../types/types";
import { fetchService } from "../services/fetchService";


const UserPage = () => {
    
    const router = useRouter();
    const getUsername = () => {
        return router.query.username?.slice(1,-1);
    }

    const [surveys,setSurveys] = useState<SurveyHeader[]>([]);

    //mock api call
    useEffect(() => {
        const rs = fetchService.getHeaders().then(
            rs => {
                setSurveys(rs.map(dto => {return {id:dto.id,name:dto.name ?? ""}}));
            },
            rs => setSurveys([])
        )
        setTimeout(()=>{setSurveys([TestSurveyHeader,TestSurveyHeader])},200);
    },[]);

    return (
    <div>
        <p>Hello, {getUsername()}</p>
        {surveys.map(s => <SurveyComponent key={s.id + Math.random() /*Random since for mocks we have same ID*/} {...s}/>)}
    </div>
    );
}

export default UserPage;