//user.html
import { useRouter } from "next/router";
import { useState } from "react";


const UserPage = () => {
    
    const getPayload = () => {
        const router = useRouter();
        return router.query.payload;
    }

    return (
    <div>
        This is a page for the user to do stuff users do.
        TEXT: {getPayload()}
    </div>
    );
}

export default UserPage;