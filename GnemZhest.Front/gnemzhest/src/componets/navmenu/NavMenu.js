import { Container, Nav, Navbar, NavDropdown} from "react-bootstrap";
import 'bootstrap/dist/css/bootstrap.min.css';
import { Routes, Route, Link } from "react-router-dom";
import Home from "../pages/Home";
import Register from "../pages/Register";
import Login from "../pages/Login";
import ProductPage from "../pages/ProductPage";

import '../navmenu/NavMenu.css'
import useAuth from "../../hooks/useAuth";

const NavMenu = () => {
  const auth = useAuth();

    return (
      <div>
      <Navbar className="navMenu" collapseOnSelect expand="lg" bg="dark" variant="dark">
      <Container>
      <Navbar.Brand as={Link} to="/">GnemZhest</Navbar.Brand>
      <Navbar.Toggle aria-controls="responsive-navbar-nav" />
      <Navbar.Collapse id="responsive-navbar-nav">
        <Nav className="me-auto">

        </Nav>
        {auth.isLoaded && auth.user? 
          <Nav>
            <NavDropdown title={`${auth.user.name} ${auth.user.surName}`}>
              <NavDropdown.Item href="#action/3.1">My profile</NavDropdown.Item>
              <NavDropdown.Item href="#action/3.2">My orders</NavDropdown.Item>
              <NavDropdown.Divider />
              <NavDropdown.Item onClick={()=>auth.logOut()}>Logout</NavDropdown.Item>
            </NavDropdown>
          </Nav>
          :
          <Nav>
            <Nav.Link as={Link} to="/login">Login</Nav.Link>
            <Nav.Link as={Link} to="/register">Register</Nav.Link>
          </Nav>
        }
      </Navbar.Collapse>
      </Container>
    </Navbar>
    
    <div>
      <Routes>
        <Route path="/" element={<Home/>}/>
        <Route path="/login" element={<Login/>}/>
        <Route path="/register" element={<Register/>}/>
        <Route path='/products/:id' element={<ProductPage/>}></Route>
      </Routes>
    </div>
    </div>
    
    )
}

export default NavMenu