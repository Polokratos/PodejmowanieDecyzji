//index.html
import React, { useState } from 'react';
import { fetchService } from '../services/fetchService';

const SignInPage = () => {

  const [username,setUsername] = useState("");
  const [password,setPassword] = useState("");
  const [loginFailed,setLoginFailed] = useState(false); 
  
  const handleLogin = () => {
      fetchService.login({username,password}).then(
        response => {
          window.sessionStorage.setItem("sessionKey",response);
          window.location.href = `/user?username=${JSON.stringify(username)}`;
        },
        reason => {
          console.log(reason);
          setLoginFailed(false);
        });
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
            <input type="submit" value="Login" onClick={handleLogin}></input>
        </div>
    </div>
  );
}

export default SignInPage;