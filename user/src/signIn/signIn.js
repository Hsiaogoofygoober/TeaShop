import React,{useState, useEffect} from "react";
import SignInForm from './signInForm';
import img from '../img/teapot.jfif';
import {api} from '../api';

const imgStyle = {
    width: 'auto',
    height: 'auto' 
  };
function SignIn() {
    const [formDone,setFormDone] = useState(0);
    const [isSubmit,setIsSubmit] = useState(false);

    /*useEffect(()=>{
        const url = api.API_URL + 'Login';
        const fetchUserData = async()=>{
            try{
                const response = await fetch(url);
                const json = await response.json();
                console.log(response);
                console.log(json);
            }
            catch(error){
                console.log("error",error);
            }
        }
        if(isSubmit===true){
            fetchUserData();
        }
        
    },[]);*/

   /* if(isSubmit===true){
        setIsSubmit(false);
        console.log(formDone);
        switch(formDone){
            case 0 : return(alert('請填寫帳號密碼'));
            case 1 : return(alert('登入成功'));
            case 2 : return(alert('帳號密碼請填寫正確'));
        }
    }*/
    return (
        <div className="uk-child-width-1-4@m uk-margin-medium-top center">
            <div className="uk-height-medium">
                <div className="uk-card uk-card-default">
                    <div className="uk-card-media-top">
                        <img src={img} style={imgStyle}/>
                    </div>
                    <SignInForm setFormDone={setFormDone} setIsSubmit={setIsSubmit}/>
                    
                </div>
            </div>
        </div>
    )
}
export default SignIn;