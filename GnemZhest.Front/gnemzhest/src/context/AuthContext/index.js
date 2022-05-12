import { createContext } from "react";

export const AuthContext = createContext({
  isLoaded: false,
  isAdmin: false,
  user: null,
  token: null,
  setUser: () => { },
  setToken: () => { },
  addItemToCart: () => { },
  logOut: () => { }
});