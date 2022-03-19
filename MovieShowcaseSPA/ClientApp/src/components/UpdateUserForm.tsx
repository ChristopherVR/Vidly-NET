import React, { useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import * as userService from '../services/userService';
import { RegisterUser, UpdateUser } from '../interfaces/user';
import UserContext from '../context/userContext';
import UserForm from './UserForm';

function UpdateUserForm() {
  const { user, setUser } = useContext(UserContext);
  const navigate = useNavigate();
  const handleSubmit = async (data: RegisterUser | UpdateUser) => {
    await userService.update(data as UpdateUser);
    if (!setUser || !user) throw new Error('User context is not defined');
    setUser({
      id: user.id,
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
  if (!user) return null;
  return (
    <UserForm submit={handleSubmit} title="Update" user={user as UpdateUser} />
  );
}

export default UpdateUserForm;
