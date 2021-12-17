import React, { useState,useRef } from "react";
import { Link } from "react-router-dom";
import {api} from '../api';
import '../index.css'

function SignInForm(props) {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    
    const handleSubmit =(e)=>{
        let json = false;
        e.preventDefault();
        props.setIsSubmit(true);
        const data = new FormData(document.getElementById("signUpForm"))
        const url = api.API_URL + 'Login';
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
            }
            catch(error){
                console.log("error",error);
            }
        }
        async function checkIfInputCorrect(){
            await fetchUserData();
            console.log(username)
            if(username !== '' && password !== ''){
                if(json === false){
                   return(alert("帳號密碼請填寫正確")) ;
                }
                else if(json === true){
                    return(alert("登入成功")) ;
                }
                
            }
            else{
                return(alert("請填寫帳號密碼"));
            }
        }
        checkIfInputCorrect();
        }

        
    return (
        <form id="signUpForm" onSubmit={handleSubmit}>
            <fieldset className="uk-fieldset">
                <legend className="uk-legend center ">茶壺倉儲管理者登錄系統</legend>
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
                    <input type="submit" value="登入" className="uk-button uk-button-default" />
                    <Link to="/signUp" className="plus_cycle" uk-icon="icon: plus-circle; ratio:1.5"></Link>
                </div>
            </fieldset>
        </form>
    )
}


export default SignInForm;