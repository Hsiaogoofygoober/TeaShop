import React,{useState} from "react";
import SignUpForm from './signUpForm';
import img from '../img/teapot.jfif'
const imgStyle = {
    width: 'auto',
    height: 'auto' 
  };
function SignUp() {
    const [formDone,setFormDone] = useState(false);
    const [isSubmit,setIsSubmit] = useState(false);

    if(isSubmit===true){
        setIsSubmit(false);
        return(formDone ? alert('登入成功'): alert('帳號密碼請填寫正確'))

    }
    return (
        <div className="uk-child-width-1-4@m uk-margin-medium-top center">
            <div className="uk-height-medium">
                <div className="uk-card uk-card-default">
                    <div className="uk-card-media-top">
                        <img src={img} style={imgStyle}/>
                    </div>
                    <SignUpForm setFormDone={setFormDone} setIsSubmit={setIsSubmit}/>
                    
                </div>
            </div>
        </div>
    )
}
export default SignUp;