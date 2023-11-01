//index.html
import React, { useState } from 'react';

const SignInPage = () => {

  const [username,setUsername] = useState("");
  const [password,setPassword] = useState("");
  const [loginFailed,setLoginFailed] = useState(false); 

  const redirect = (payload) => {
    payload = {username:username, ...payload}
    window.location.href = `/user?payload=${payload}`
  }

  const sendFurther = () => {
    return fetch("http://server.com/login",
    {
      method : "POST",
      body : JSON.stringify({username:username,password:password})
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