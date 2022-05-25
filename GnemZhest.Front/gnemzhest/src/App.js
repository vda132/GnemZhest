import NavMenu from './componets/navmenu/NavMenu';
import './App.css';
import { ToastContainer } from 'react-toastify';
import "react-toastify/dist/ReactToastify.css";

function App() {
  return (
    <div className='main'>
      <ToastContainer />
      <div>
        <NavMenu />
      </div>
    </div>
  );
}

export default App;
