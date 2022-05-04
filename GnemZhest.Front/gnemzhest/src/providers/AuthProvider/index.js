import { useCallback, useEffect, useMemo, useState } from "react";
import { AuthContext } from "../../context/AuthContext";
 
function AuthProvider(props) {
    const [isLoaded, setIsLoaded] = useState(false);
    const [user, setUserData] = useState(null);
    const [token, setTokenData] = useState(null);

    const setUser = useCallback((tokenData) => {
      //todo: call api to get user data
    });

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
    }, [setToken]);
    
    const loadData = useCallback(async () =>{
        const tokenData = localStorage.getItem("token");
        setTokenData(tokenData);
        //todo: get user from api
        try {
            //const { data };
            //setUserData(data);
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
          user,
          token,
          setUser,
          setToken,
          logOut,
        }),
        [isLoaded, user, token, setToken, logOut]
      );

      return (
        <AuthContext.Provider value={contextValue}>
          {props.children}
        </AuthContext.Provider>
      );
}

export default AuthProvider