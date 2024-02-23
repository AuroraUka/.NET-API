import { useState, useEffect } from "react";
import { PhoneBook } from "../../app/models/phoneBook"
import { Box, Button, Paper, TableContainer, Table, TableHead, TableRow, TableCell, TableBody } from "@mui/material";
import BasicModal from "./Modal";
import axios from "axios";
import { usePhoneBookContext } from '../../app/context/PhoneBookContext';


const PhoneBook=() => {
    
  const [phonebooks, setPhonebooks] = useState<PhoneBook[]>([]);
  const { shouldRefetch, setShouldRefetch } = usePhoneBookContext();
 
  useEffect(() => {
    if (shouldRefetch) {
        fetchData('');
        setShouldRefetch(false);
    }
}, [shouldRefetch, setShouldRefetch]);

  function fetchData(path: string) {
    axios.get(`http://localhost:5005/api/PhoneBooks/${path}`)
      .then(response => setPhonebooks(response.data))
      .catch(error => console.error('Error fetching data:', error));
  }
  
  function deleteData(id: number | undefined){
    axios.delete(`http://localhost:5005/api/PhoneBooks/${id}`)
    .then(() => {
      fetchData('');
    })
    .catch(error => console.error('Error deleting data:', error));
  }
  return (
    <>
      <Box display='flex' sx={{ flexDirection:'column', width: '100%' ,justifyContent:'center', alignItems:'center'}}>
            <BasicModal buttonText="ADD"  />
      <Paper sx={{ width: '70%', mb: 2 }}>
        <TableContainer sx={{display: 'flex', flexDirection: 'column'}}>
        <Table 
            sx={{ minWidth: 750 }}
            size="small"
          >
            <TableHead>
              <TableRow >
                <TableCell colSpan={6} ><button onClick={()=>fetchData('firstName')}>First Name</button></TableCell>
                <TableCell colSpan={6}><button onClick={()=>fetchData('lastName')}>Last Name</button></TableCell>
                <TableCell colSpan={6}>Phone Type</TableCell>
                <TableCell colSpan={6}>Phone Number</TableCell>
                <TableCell colSpan={6} align="center" >Modify</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
            {phonebooks.map(phonebook => (
              <TableRow key={phonebook.id} sx={{ '&:hover': { backgroundColor: '#f5f5f5' },height: 33, padding:1,margin:1  }}>
                <TableCell colSpan={6}  align="left">{phonebook.firstName}</TableCell>
                <TableCell colSpan={6}  align="left">{phonebook.lastName}</TableCell>
                <TableCell colSpan={6}  align="left">{phonebook.type}</TableCell>
                <TableCell colSpan={6}  align="left">{phonebook.number}</TableCell>
                <TableCell colSpan={6}  align="center" sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center',columnGap:'10%'}}>
                  <BasicModal buttonText="EDIT"  phoneBook={phonebook} />
                  <Button 
                  variant="outlined" 
                  sx={{color: 'red', borderColor: 'red', '&:hover':{borderColor: 'red', bgcolor:'red', color:'white'}}}
                  onClick={() => deleteData(phonebook.id)}>
                    Delete 
                    </Button>
                </TableCell>
              </TableRow>
               )  )}
            </TableBody>
            </Table>
        </TableContainer>
      </Paper>
    </Box>
    </>
  )
}
export default PhoneBook;
