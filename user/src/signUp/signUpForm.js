import React, { useState } from "react";
import { Link } from "react-router-dom";
import '../index.css'

function SignUpForm(props) {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const handleSubmit =(e)=>{
        e.preventDefault();
        props.setIsSubmit(true);
        if(username!==''&&password!==''){
            props.setFormDone(true);
        }
        else{
            props.setFormDone(false);
        }
    }
    return (
        <form onSubmit={handleSubmit}>
            <fieldset className="uk-fieldset">
                <legend className="uk-legend center ">註測帳號</legend>
                <div className=" uk-inline uk-margin-small  input-center" >
                    <span className="uk-form-icon" uk-icon="icon: user"></span>
                    <input className="uk-input"  type="text" placeholder="管理者帳號"
                    onChange={(e) => setUsername(e.target.value)} value={username} />
                </div>
                <div className="uk-inline input-center uk-margin-small">
                    <span className="uk-form-icon" uk-icon="icon: lock"></span>
                    <input className="uk-input" type="password" placeholder="管理者密碼" 
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