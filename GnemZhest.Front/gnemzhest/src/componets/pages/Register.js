import React, { useState } from "react";
import validator from 'validator';
import Input from 'react-phone-number-input/input'
import { useNavigate } from "react-router-dom";
import useAuth from "../../hooks/useAuth";

import "../pages/styles.css";

function Register() {
    const [name, setName] = useState("");
    const [surname, setSurname] = useState("");
    const [login, setLogin] = useState("");
    const [phone, setPhone] = useState("");
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [error, setError] = useState("");
  
    const auth = useAuth();

    let navigate = useNavigate();
  
    const handleValidation = async (event) => {
      let formIsValid = true;

      setError("");

      if (name.length === 0) {
        formIsValid = false;
        setError("Name is not valid");
        return formIsValid;
      } 

      if (surname.length === 0) {
        formIsValid = false;
        setError("Surname is not valid");
        return formIsValid;
      } 

      if (login.length === 0) {
        formIsValid = false;
        setError("Login is not valid");
        return formIsValid;
      } 

      if (!validator.isMobilePhone(phone)) {
        formIsValid = false;
        setError("Phone is not valid");
        return formIsValid;
      } 

      if (!validator.isEmail(email)) {
        formIsValid = false;
        setError("Email Not Valid");
        return formIsValid;
      } else {
        setError("");
        formIsValid = true;
      }
  
      if (password.match(/.{8,22}/)) {
          setError("");
          formIsValid = true;
      } else {
          formIsValid = false;
          setError("Password should contain letters and length must best min 8 characters");
      }
  
      return formIsValid;
    };
  
    const registrationSubmit = async (e) => {
      e.preventDefault();

      if(await handleValidation()){
        ///register
          const response = await fetch(`https://localhost:5001/api/Users/register`, {
            method: 'POST',
            headers: {'Content-Type':'application/json'},
            body: JSON.stringify({
              "name":name,
              "surName":surname,
              "login":login,
              "password":password,
              "email":email,
              "phone": phone,
              "role":'User'
            })
          });

          const data = await response.json();

          if (data.status === 200) {
            //login
            const response = await fetch(`https://localhost:5001/api/Users/login`, {
              method: 'POST',
              headers: {'Content-Type':'application/json'},
              body: JSON.stringify({
                "login":login,
                "password":password
              })
            })

            const data = await response.json();
            
            //authorization
            const authResponse = await fetch(`https://localhost:5001/api/Authorization/auth/me`, {
              method: 'GET',
              headers :{
                'Content-Type':'application/json',
                'Authorization':`Bearer ${data.token}`
              }
            })
            
            const userData = await authResponse.json();
            
            auth.setToken(data.token);
            auth.setUser(userData);
            navigate(`/`);
          } else {
            setError(data.message);
          }
      }
  };
  
    return (
      <div className="App">
        <div className="container">
          <div className="row d-flex justify-content-center">
            <div className="col-md-4">
              <form id="loginform" onSubmit={registrationSubmit}>
              <div className="form-group">
                <label>Name</label>
                <input
                  type="Name"
                  className="form-control"
                  id="NameInput"
                  name="NameInput"
                  aria-describedby="nameHelp"
                  placeholder="Enter name"
                  value={name}
                  onChange={(event) => setName(event.target.value)}
                />
              </div>
              <div className="form-group">
                <label>Surname</label>
                <input
                  type="Surname"
                  className="form-control"
                  id="SurnameInput"
                  name="SurnameInput"
                  aria-describedby="surnameHelp"
                  placeholder="Enter surname"
                  value={surname}
                  onChange={(event) => setSurname(event.target.value)}
                />
              </div>
              <div className="form-group">
                <label>Phone</label>
                <Input
                    className="form-control"
                    country="UA"
                    international
                    withCountryCallingCode
                    value={phone}
                    onChange={setPhone}
                    />
              </div>
              <div className="form-group">
                <label>Login</label>
                <input
                  type="login"
                  className="form-control"
                  id="LoginInput"
                  name="LoginInput"
                  aria-describedby="loginHelp"
                  placeholder="Enter login"
                  value={login}
                  onChange={(event) => setLogin(event.target.value)}
                />
              </div>
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
                <div className="form-group-password" style={{marginTop:"10px"}}>
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
                  Register
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

export default Register;