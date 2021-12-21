import React, { useState } from "react";
import { Link , useNavigate } from "react-router-dom";
import '../index.css'
import {api} from '../api';

function SignUpForm() {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const navigate = useNavigate();
    
    const handleSubmit =(e)=>{
        let json = 0;
        e.preventDefault();
        
        const data = new FormData(document.getElementById("signUpForm"))
        const url = api.API_URL + 'Register';
        const value = Object.fromEntries(data.entries());
        const fetchUserData = async()=>{
            try{
                const response = await fetch(url, { 
                    method: 'post', 
                    headers: {'Content-Type': 'application/json'},
                    body:
                    JSON.stringify(value)
                    })
                    
                json = await response.json();
                
                console.log(response);
                console.log(json);
                console.log(value);
            }
            catch(error){
                console.log("error",error);
            }
        }

        if(username === '' || password === ''){
            return(alert("請填寫帳號密碼"));
        }

        async function checkIfInputCorrect(){
            await fetchUserData();
            console.log(username)
            console.log(password)
            if(username !== '' && password !== ''){
                if(json === 0){
                   return(alert("註冊失敗")) ;
                }
                else if(json === 1){
                    alert("註冊成功") ;
                    navigate('../');
                }
                
            }
        }
        checkIfInputCorrect();
        }
    return (
        <form id="signUpForm" onSubmit={handleSubmit}>
            <fieldset className="uk-fieldset">
                <legend className="uk-legend center ">註冊帳號</legend>
                <div className=" uk-inline uk-margin-small  input-center" >
                    <span className="uk-form-icon" uk-icon="icon: user"></span>
                    <input name = "UserName" className="uk-input"  type="text" placeholder="管理者帳號"
                    onChange={(e) => setUsername(e.target.value)} value={username} />
                </div>
                <div className="uk-inline input-center uk-margin-small">
                    <span className="uk-form-icon" uk-icon="icon: lock"></span>
                    <input name = "Password" className="uk-input" type="password" placeholder="管理者密碼" 
                    onChange={(e) => setPassword(e.target.value)} value={password}/>
                </div>
                <div className="uk-margin-small center">
                    <input type="submit" value="註冊" className="uk-button uk-button-default" />
                    <Link to="/" className="plus_cycle" uk-icon="icon: sign-in; ratio:1.5"></Link>
                </div>
            </fieldset>
        </form>
    )
}


export default SignUpForm;