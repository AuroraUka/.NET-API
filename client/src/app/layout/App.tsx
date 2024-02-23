import { Box, List, ListItem, Typography } from "@mui/material";
import PhoneBook from "../../features/PhoneBook/PhoneBook";
import { ToastContainer } from "react-toastify";
import 'react-toastify/dist/ReactToastify.css'

function App() {

  return (
    <>
    <ToastContainer position="bottom-right" theme="colored"/>
        <Typography variant='h3' align="center" p={2}>TASK - Aurora Uka</Typography>
        <PhoneBook />
        <Typography variant='h3' align="center" p={2}>Assumptions</Typography>
        <Box sx={{display:'flex',justifyContent:'center', alignItems:'center'}}>
        <List >
          <ListItem >
            1. The API can be accessed without verification.
          </ListItem>
          <ListItem >
            2. The User must be notified when a change has been made.
          </ListItem>
          <ListItem >
            3. The data must be validated.
          </ListItem>
          <ListItem >
            4. The API can have multiple threads for fetching data.
          </ListItem>
          <ListItem >
            5. The API and UI should be in different servers. CORS must be configured.
          </ListItem>
          <ListItem >
            6. Sorting should be done at the database level.
          </ListItem>
          <ListItem >
            7. Database contains only the table of the Phone Books.
          </ListItem>
          <ListItem >
            8. The code must be uploaded to GitHub, to verify the code-first approach requirement.
          </ListItem>
          <ListItem >
            9. Usage of icons to make the interface more user-friendly.
          </ListItem>
          <ListItem >
            10. The phone number is saved as a string data type.
          </ListItem>
          <ListItem >
            11. Database migrations to manage database versions.
          </ListItem>
        </List></Box>
    </>
  );
}

export default App
