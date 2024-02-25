import  { useState, ChangeEvent, FormEvent, useEffect } from "react";
import { TextField, Button, MenuItem, Select, SelectChangeEvent, Box } from "@mui/material";
import axios from "axios";
import { PhoneBook } from "../../app/models/phoneBook";
import { usePhoneBookContext } from '../../app/context/PhoneBookContext';
import { toast } from "react-toastify";


interface Props {
    open: boolean;
    handleClose: () => void;
    phoneBook?: PhoneBook;
    buttonText: string;
  }
enum PhoneBookType {
    Work = "Work",
    Home = "Home",
    Cellphone = "Cellphone"
  }

// eslint-disable-next-line @typescript-eslint/no-unused-vars
const PhoneBookForm= ({ handleClose, phoneBook, buttonText } :Props) => {

  const [values, setValues] = useState<PhoneBook>({
    firstName: "",
    lastName: "",
    type: PhoneBookType.Work,
    number: ""
  });
  const {  setShouldRefetch } = usePhoneBookContext();

  useEffect(() => {
    if (phoneBook) {
      setValues({
        ...phoneBook,
        type: phoneBook.type || PhoneBookType.Work 
    });
  }
  }, [phoneBook]);

  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setValues({ ...values, [name]: value });
  };

  const handleSelectChange = (e: SelectChangeEvent<string>) => {
    setValues({ ...values, type: e.target.value });
  };
  

  const handleSubmit = (e: FormEvent) => {
    e.preventDefault();
    if (buttonText === 'ADD') {
      axios.post("http://localhost:5005/api/PhoneBooks/", values)
      .then(() => {
        setValues({
          firstName: "",
          lastName: "",
          type: PhoneBookType.Work,
          number: ""
        });
        handleClose();
        setShouldRefetch(true);
        toast.success("The entity has been added successfully!");
      })
      .catch(error => {
        console.error('Error adding data:', error)
        toast.error("The has been an error! Entity not added!");
      
      });
    } else if (buttonText === 'EDIT' && values.id) {
      axios.put(`http://localhost:5005/api/PhoneBooks/${phoneBook?.id}`, values)
      .then(() => {
        handleClose();
        setShouldRefetch(true);
        toast.info("The entity has been updated successfully!");
      })
      .catch(error => {
        console.error('Error updating data:', error)
        toast.error("The has been an error! Update unsuccessfull!");
      });
    }
  };


  return (
    <form onSubmit={handleSubmit}>
      <TextField
        name="firstName"
        label="First Name"
        placeholder="First Name"
        value={values.firstName}
        onChange={handleChange}
        margin="normal"
        fullWidth
        required
        inputProps={{ pattern: '[A-Za-z]*' }}
      />
      <TextField
        name="lastName"
        label="Last Name"
        placeholder="Last Name"
        value={values.lastName}
        onChange={handleChange}
        margin="normal"
        fullWidth
        required
        inputProps={{ pattern: '[A-Za-z]*' }}
      />
       <Select
        name="type"
        label="Phone Type"
        value={values.type}
        onChange={handleSelectChange}
        fullWidth
        required
      >
        {Object.values(PhoneBookType).map(type => (
          <MenuItem key={type} value={type}>
            {type}
          </MenuItem>
        ))}
      </Select>
      <TextField
        name="number"
        label="Phone Number"
        value={values.number}
        placeholder="123456789"
        onChange={handleChange}
        margin="normal"
        fullWidth
        required
        inputProps={{ inputMode: 'numeric', pattern: '[0-9]*' }}
      />
      <Box display={`flex`} justifyContent={`space-around`}>
      <Button type="submit" variant="outlined" color="error" onClick={handleClose}>
        Cancel
      </Button>
      <Button type="submit" variant="contained" color="primary">
        {buttonText} 
      </Button>
      </Box>
    </form>
  );
};

export default PhoneBookForm;
