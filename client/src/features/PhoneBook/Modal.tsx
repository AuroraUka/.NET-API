import {  useState } from 'react';
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import Modal from '@mui/material/Modal';
import PhoneBookForm from './Form';
import { PhoneBook } from '../../app/models/phoneBook';
import EditIcon from '@mui/icons-material/Edit';
import AddIcon from '@mui/icons-material/Add';
interface ModalProps {
  buttonText: string;
  phoneBook?: PhoneBook;
}


const style = {
  position: 'absolute' ,
  top: '50%',
  left: '50%',
  transform: 'translate(-50%, -50%)',
  width: 300,
  bgcolor: 'background.paper',
  border: '2px solid #000',
  boxShadow: 24,
  p: 4,
};

const BasicModal = ({ buttonText, phoneBook } : ModalProps) => {
  const [open, setOpen] = useState(false);
  const handleOpen = () => setOpen(true);
  const handleClose = () => setOpen(false);


  return (
    <div>
        {buttonText == 'ADD' ? <Button 
        variant="contained" 
        sx={{ m: 1, bgcolor:'#37b921', '&:hover': {bgcolor:'#0aaa01'}}} 
        onClick={handleOpen}>{buttonText}<AddIcon/></Button> 
        :
        <Button 
        variant='contained'
        onClick={handleOpen}><EditIcon/></Button>}
      <Modal
        open={open}
        onClose={handleClose}
      >
        <Box sx={style}>
          <Typography id="modal-modal-title" variant="h6" component="h2" align='center'>
            {buttonText} Phone Book
          </Typography>
          <PhoneBookForm open={open} handleClose={handleClose}  buttonText={buttonText} phoneBook={phoneBook} />
        </Box>
      </Modal>
    </div>
  );
}

export default BasicModal;
