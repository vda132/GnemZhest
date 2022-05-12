import React, { useState } from "react";
import "../pages/styles.css";
import { Link, useNavigate } from "react-router-dom";
import useAuth from "../../hooks/useAuth";
import { Button } from "react-bootstrap";



function Login() {
  const [password, setPassword] = useState("");
  const [login, setLogin] = useState("");
  const [error, setError] = useState("");

  const auth = useAuth();

  let navigate = useNavigate();

  const handleValidation = (event) => {
    let formIsValid = true;

    if (login.length === 0) {
      formIsValid = false;
      setError("Login Not Valid");
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

  const loginSubmit = async (e) => {
    e.preventDefault();

    if (handleValidation()) {
      const response = await fetch(`https://localhost:5001/api/Users/login`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({
          "login": login,
          "password": password
        })
      })

      const data = await response.json();

      if (data.status === 200) {
        //authorization
        const authResponse = await fetch(`https://localhost:5001/api/Authorization/auth/me`, {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${data.token}`
          }
        })
        const userData = await authResponse.json();

        auth.setToken(data.token);
        auth.setUser(userData);
        navigate(`/`);
      } else {
        setError(`Login or password is incorrect`);
      }
    }
  };

  return (
    <div className="App">
      <div className="container">
        <div className="row d-flex justify-content-center">
          <div className="col-md-4">
            <form id="loginform" onSubmit={loginSubmit}>
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
              <Button type="submit" className="btn btn-primary" style={{ marginTop: "5%" }}>
                Login
              </Button>
              <div>
                <small id="error" className="text-danger form-text">
                  {error}
                </small>
              </div>
            </form>
            <Link to="/register">Create account</Link>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Login;
