import React, { useState } from "react";
import validator from 'validator';
import "../pages/styles.css";
import "bootstrap/dist/css/bootstrap.min.css";
import { useNavigate } from "react-router-dom";

function Login() {
  const [password, setPassword] = useState("");
  const [email, setEmail] = useState("");
  const [error, setError] = useState("");

  let navigate = useNavigate();

  const handleValidation = (event) => {
    let formIsValid = true;

    if (!validator.isEmail(email)) {
      formIsValid = false;
      setError("Email Not Valid");
      return false;
    } else {
      setError("");
      formIsValid = true;
    }

    if (password.match(/.{8,22}/)) {
        setError("");
        formIsValid = true;
    } else {
        formIsValid = false;
        setError(
        "Password should contain letters and length must best min 8 characters"
        );
    }

    return formIsValid;
  };

  const loginSubmit = (e) => {
    e.preventDefault();
    if(handleValidation()){
        console.log(email, password);
        navigate(`/`);
    }
};
  return (
    <div className="App">
      <div className="container">
        <div className="row d-flex justify-content-center">
          <div className="col-md-4">
            <form id="loginform" onSubmit={loginSubmit}>
              <div className="form-group">
                <label>Email address</label>
                <input
                  type="email"
                  className="form-control"
                  id="EmailInput"
                  name="EmailInput"
                  aria-describedby="emailHelp"
                  placeholder="Enter email"
                  value={email}
                  onChange={(event) => setEmail(event.target.value)}
                />
              </div>
              <div className="form-group-password">
                <label>Password</label>
                <input
                  type="password"
                  className="form-control"
                  id="exampleInputPassword1"
                  placeholder="Password"
                  value={password}
                  onChange={(event) => setPassword(event.target.value)}
                />
              </div>
              <button type="submit" className="btn btn-primary" style={{marginTop:"5%"}}>
                Login
              </button>
            </form>
          </div>
          <small id="error" className="text-danger form-text">
                  {error}
                </small>
        </div>
      </div>      
    </div>
  );
}
export default Login;
