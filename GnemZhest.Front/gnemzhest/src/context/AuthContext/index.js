import { createContext } from "react";

export const AuthContext = createContext({
  isLoaded: false,
  isAdmin: false,
  user: null,
  token: null,
  cartItems:[],
  setUser: () => { },
  setToken: () => { },
  addItemToCart: () => { },
  decreaseItemFromCart: () => { },
  removeItemFromCart: () => { },
  logOut: () => { }
});