import React, { useContext, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import * as userService from '../services/userService';
import { RegisterUser, UpdateUser } from '../interfaces/user';
import UserContext from '../context/userContext';
import UserForm from './UserForm';

function RegisterForm() {
  const { setUser: setRegisteredUser } = useContext(UserContext);
  const navigate = useNavigate();
  const [user] = useState<RegisterUser>({
    name: '',
    surname: '',
    userName: '',
    password: '',
    address: '',
    homeNumber: '',
    phoneNumber: '',
  });

  const handleSubmit = async (data: RegisterUser | UpdateUser) => {
    const id = await userService.register(data as RegisterUser);
    if (setRegisteredUser)
      setRegisteredUser({
        id,
        name: data.name,
        surname: data.surname,
        userName: data.userName,
        imageUrl: data.imageUrl,
        address: data.address,
        homeNumber: data.homeNumber,
        phoneNumber: data.phoneNumber,
      });
    navigate('/');
  };
  return <UserForm submit={handleSubmit} title="Register" user={user} />;
}

export default RegisterForm;
