import React, { useState } from "react";
import './index.css'

function Form(props) {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const handleSubmit =(e)=>{
        e.preventDefault();
        props.setIsSubmit(true);
        if(username!=''&&password!=''){
            props.setFormDone(true);
        }
        else{
            props.setFormDone(false);
        }
    }
    return (
        <form onSubmit={handleSubmit}>
            <fieldset className="uk-fieldset">
                <legend className="uk-legend center ">茶壺倉儲管理者登錄系統</legend>
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
                    <input type="submit" value="登入" className="uk-button uk-button-default" />
                    <a href="./signUp.html" uk-icon="icon: plus-circle; ratio:1.5" className="plus_cycle"></a>
                </div>
            </fieldset>
        </form>
    )
}


export default Form;