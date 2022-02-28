import React, { useContext, useState } from 'react';
import { MDBBtn, MDBInput, MDBValidation } from 'mdb-react-ui-kit';
import * as userService from '../services/userService';
import { RegisterUser } from '../interfaces/user';
import UserContext from '../context/userContext';

function RegisterForm() {
  const { setUser: setRegisteredUser } = useContext(UserContext);
  const [user, setUser] = useState<RegisterUser>({
    name: '',
    surname: '',
    userName: '',
    password: '',
    address: '',
    homeNumber: '',
    personalNumber: '',
  });

  const onChangeHandler = (ev: HTMLInputElement) => {
    setUser({
      ...user,
      [ev.name]: ev.value,
    });
  };

  const handleSubmit = async () => {
    const id = await userService.register(user);
    setRegisteredUser({
      id,
      name: user.name,
      surname: user.surname,
      userName: user.userName,
    });
  };
  return (
    <div>
      <h1>Register</h1>
      <MDBValidation id="register-form" onSubmit={handleSubmit}>
        <MDBInput
          name="username"
          label="Username"
          type="text"
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
          value={user.surname}
          onChange={({ currentTarget }: { currentTarget: HTMLInputElement }) =>
            onChangeHandler(currentTarget)
          }
          required
          invalid
          validation="Surname is required"
        />
        <MDBBtn form="register-form" type="submit">
          Submit
        </MDBBtn>
      </MDBValidation>
    </div>
  );
}

export default RegisterForm;
