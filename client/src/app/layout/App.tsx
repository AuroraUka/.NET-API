import { Typography } from "@mui/material";
import PhoneBook from "../../features/PhoneBook/PhoneBook";
import { ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css'

function App() {

  return (
    <>
    <ToastContainer position="bottom-right" theme="colored"/>
        <Typography variant='h3' align="center" p={2}>TASK - Aurora Uka</Typography>
        <PhoneBook />
    </>
  );
}

export default App
