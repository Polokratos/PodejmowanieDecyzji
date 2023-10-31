// index.html
import React, { useState } from 'react';

export default function SignInPage() {

  const [username,setUsername] = useState("");
  const [password,setPassword] = useState("")
  
  const sendFurther = async () => {
    const fd :FormData = new FormData()
    fd.append("username",username)
    fd.append("password",password)

    const rs = await fetch("http://example.com/movies.json",{
      method : "POST",
      body : fd
    }).catch(()=>console.log(fd)) 
  }

  return (
    <div className="container">
        <h2>Login</h2>
        <div>
            <label htmlFor="username">
              Username:
              <input type="text" value={username} onChange={(e) => setUsername(e.target.value)}></input>
            </label>
            <label htmlFor="password">Password:
              <input type="password" value={password} onChange={(e) => setPassword(e.target.value)}></input>
            </label>
            <input type="submit" value="Login" onClick={sendFurther}></input>
        </div>
    </div>
  );
}
