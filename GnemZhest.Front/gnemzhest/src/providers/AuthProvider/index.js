import { useCallback, useEffect, useMemo, useState } from "react";
import { AuthContext } from "../../context/AuthContext";
import { toast } from "react-toastify";

function AuthProvider(props) {
  const [isLoaded, setIsLoaded] = useState(false);
  const [isAdmin, setIsAdmin] = useState(false);
  const [user, setUser] = useState(null);
  const [token, setTokenData] = useState(null);
  const [cartItems, setCartItems] = useState(localStorage.getItem("cartItems") ? JSON.parse(localStorage.getItem("cartItems")) : []);
  const [cartAmmount, setCartAmmount] = useState(localStorage.getItem("cartItems") ? JSON.parse(localStorage.getItem("cartItems")).length : 0)
  const [cartPrice, setCartPrice] = useState(0);

  const setToken = useCallback((tokenData) => {
    setTokenData(tokenData);

    if (tokenData) {
      localStorage.setItem("token", tokenData);
    } else {
      localStorage.removeItem("token");
    }
  }, []);

  const addItemToCart = useCallback((product) => {
    let cartItem = {};
    const productExist = cartItems.find(elem => elem.item.id === product.id);
    if (productExist) {
      productExist.ammount += 1;
      setCartItems(cartItems.map((item) =>
        item.item.id === productExist.item.id
          ? { ...productExist } : item));

      toast.info(`Increased ${product.name} quantity`, {
        position: "bottom-left",
      });
      
    } else {
      let array = cartItems;
      cartItem = {
        item: product,
        ammount: 1
      };
      array.push(cartItem);
      setCartItems(array);
      toast.success(`${product.name} added to cart`, {
        position: "bottom-left",
      });
    }

    localStorage.setItem("cartItems", JSON.stringify(cartItems));
    setCartAmmount(cartItems.length);
    let totalPrice = 0;
    cartItems.map((item) =>{
      totalPrice += (item.item.price * item.ammount);
    });
    setCartPrice(totalPrice);
  }, [cartItems, cartAmmount, cartPrice, setCartPrice, setCartAmmount]);

  const decreaseItemFromCart = useCallback((product) => {
    const productExist = cartItems.find(elem => elem.item.id === product.id);

    productExist.ammount -= 1;
    setCartItems(cartItems.map((item) =>
      item.item.id === productExist.item.id
        ? { ...productExist } : item))
    toast.info(`decreased ${product.name} quantity`, {
      position: "bottom-left",
    });
    localStorage.setItem("cartItems", JSON.stringify(cartItems));
    setCartAmmount(cartItems.length);
    let totalPrice = 0;
    cartItems.map((item) =>{
      totalPrice += (item.item.price * item.ammount);
    })
    setCartPrice(totalPrice);
  }, [cartItems, cartAmmount, cartPrice, setCartPrice, setCartAmmount]);

  const removeItemFromCart = useCallback((product) => {
    const newCartItems = cartItems.filter((item) => item.item.id !== product.id);
    setCartItems(newCartItems);
    localStorage.setItem("cartItems", JSON.stringify(newCartItems));
    setCartAmmount(newCartItems.length);
    toast.error(`${product.name} removed from cart`, {
      position: "bottom-left",
    });
    let totalPrice = 0;
    newCartItems.map((item) =>{
      totalPrice += (item.item.price * item.ammount);
    })
    setCartPrice(totalPrice);
  }, [cartItems, cartAmmount, cartPrice, setCartPrice, setCartAmmount]);

  const removeItems = useCallback(() => {
    setCartItems([]);
    setCartAmmount(0);
    setCartPrice(0);
    localStorage.removeItem("cartItems");
  }, [cartItems, cartAmmount, cartPrice, setCartItems, setCartPrice, setCartAmmount])

  const logOut = useCallback(() => {
    setUser(null);
    setToken(null);
    setIsLoaded(false);
    setIsAdmin(false);
    setCartItems([]);
    setCartAmmount(0);
    localStorage.removeItem("cartItems");
  }, [setToken, setCartItems, setUser, setIsLoaded, setIsAdmin]);

  const loadData = useCallback(async () => {
    const tokenData = localStorage.getItem("token");
    setTokenData(tokenData);
    let totalPrice = 0;
    cartItems.map((item) =>{
      totalPrice += (item.item.price * item.ammount);
    })
    setCartPrice(totalPrice);
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

        setUser(userData);
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
      cartItems,
      cartAmmount,
      cartPrice,
      setUser,
      setCartItems,
      setToken,
      addItemToCart,
      decreaseItemFromCart,
      removeItemFromCart,
      removeItems,
      setCartPrice,
      logOut,
    }),
    [isLoaded, isAdmin, user, token, cartItems, cartPrice, cartAmmount, setToken, setIsAdmin, setIsLoaded, addItemToCart, decreaseItemFromCart, removeItemFromCart, removeItems, logOut]
  );

  return (
    <AuthContext.Provider value={contextValue}>
      {props.children}
    </AuthContext.Provider>
  );
}

export default AuthProvider