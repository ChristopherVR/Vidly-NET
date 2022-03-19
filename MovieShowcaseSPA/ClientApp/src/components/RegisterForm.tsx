import React, { useContext, useState } from 'react';
import { MDBBtn, MDBInput, MDBValidation } from 'mdb-react-ui-kit';
import { useNavigate } from 'react-router-dom';
import * as userService from '../services/userService';
import { RegisterUser } from '../interfaces/user';
import UserContext from '../context/userContext';

function RegisterForm() {
  const { setUser: setRegisteredUser } = useContext(UserContext);
  const navigate = useNavigate();
  const [user, setUser] = useState<RegisterUser>({
    name: '',
    surname: '',
    userName: '',
    password: '',
    address: '',
    homeNumber: '',
    phoneNumber: '',
  });

  const onChangeHandler = (ev: HTMLInputElement) => {
    setUser({
      ...user,
      [ev.name]: ev.value,
    });
  };

  const handleSubmit = async () => {
    const id = await userService.register(user);
    if (setRegisteredUser)
      setRegisteredUser({
        id,
        name: user.name,
        surname: user.surname,
        userName: user.userName,
        imageUrl: user.imageUrl,
      });
    navigate('/');
  };
  return (
    <div>
      <h1>Register</h1>
      <MDBValidation
        className="mt-2"
        id="register-form"
        onSubmit={handleSubmit}
      >
        <MDBInput
          name="userName"
          label="Username"
          type="text"
          className="mb-2"
          value={user.userName}
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
          required
          invalid
          validation="Username is required"
        />
        <MDBInput
          name="password"
          label="Password"
          type="password"
          autoComplete="on"
          className="mb-2"
          value={user.password}
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
          required
          invalid
          validation="Password is required"
        />
        <MDBInput
          name="name"
          label="Name"
          type="text"
          className="mb-2"
          value={user.name}
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
          required
          invalid
          validation="Name is required"
        />
        <MDBInput
          name="surname"
          label="Surname"
          type="text"
          className="mb-2"
          value={user.surname}
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
          required
          invalid
          validation="Surname is required"
        />
        <MDBInput
          name="homeNumber"
          label="Home Number"
          type="tel"
          className="mb-2"
          value={user.homeNumber}
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
          required
          invalid
          validation="Home Number is required"
        />
        <MDBInput
          name="phoneNumber"
          label="Phone Number"
          type="tel"
          className="mb-2"
          value={user.phoneNumber}
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
          required
          invalid
          validation="Phone Number is required"
        />
        <MDBInput
          name="address"
          label="Address"
          type="text"
          className="mb-2"
          value={user.address}
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
          required
          invalid
          validation="Address is required"
        />
        <MDBInput
          name="imageUrl"
          label="Image Url"
          type="url"
          className="mb-2"
          value={user.imageUrl}
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
        />
        <MDBBtn form="register-form" type="submit">
          Submit
        </MDBBtn>
      </MDBValidation>
    </div>
  );
}

export default RegisterForm;
