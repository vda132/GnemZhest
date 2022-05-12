import { useCallback, useEffect, useMemo, useState } from "react";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../context/AuthContext";

function AuthProvider(props) {
  const [isLoaded, setIsLoaded] = useState(false);
  const [isAdmin, setIsAdmin] = useState(false);
  const [user, setUser] = useState(null);
  const [token, setTokenData] = useState(null);

  let navigate = useNavigate();

  const setToken = useCallback((tokenData) => {
    setTokenData(tokenData);

    if (tokenData) {
      localStorage.setItem("token", tokenData);
    } else {
      localStorage.removeItem("token");
    }
  }, []);

  const logOut = useCallback(() => {
    setUser(null);
    setToken(null);
    navigate('/login');
  }, [setToken]);

  const loadData = useCallback(async () => {
    const tokenData = localStorage.getItem("token");
    setTokenData(tokenData);

    try {
      if (tokenData) {
        const authResponse = await fetch(`https://localhost:5001/api/Authorization/auth/me`, {
          method: 'GET',
          headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${tokenData}`
          }
        })

        const userData = await authResponse.json();
        
        if (userData.role === 'Admin')
          setIsAdmin(true);

        setUser(userData)
      }
    } catch {
      setToken(null);
    } finally {
      setIsLoaded(true);
    }
  }, [setToken]);

  useEffect(() => {
    loadData();
  }, [loadData]);

  const contextValue = useMemo(
    () => ({
      isLoaded,
      isAdmin,
      user,
      token,
      setUser,
      setToken,
      logOut,
    }),
    [isLoaded, isAdmin, user, token, setToken, logOut]
  );

  return (
    <AuthContext.Provider value={contextValue}>
      {props.children}
    </AuthContext.Provider>
  );
}

export default AuthProvider