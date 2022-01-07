import React,{useState, useEffect} from "react";
import SignInForm from './signInForm';
import img from '../img/teapot.jfif';


const imgStyle = {
    width: 'auto',
    height: 'auto' 
  };
function SignIn() {
    
    return (
        
        <div className="uk-child-width-1-4@m uk-margin-medium-top center">
            <div className="uk-height-medium">
                <div className="uk-card uk-card-default">
                    <div className="uk-card-media-top">
                        <img src={img} style={imgStyle}/>
                    </div>
                    <SignInForm />
                    
                </div>
            </div>
        </div>
        
    )
}
export default SignIn;