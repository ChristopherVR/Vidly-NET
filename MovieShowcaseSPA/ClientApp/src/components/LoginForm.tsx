import {
  MDBBtn,
  MDBInput,
  MDBValidation,
  MDBValidationItem,
} from 'mdb-react-ui-kit';
import React, { useState, useContext } from 'react';
import { useNavigate } from 'react-router-dom';
import UserContext from '../context/userContext';
import auth from '../services/authService';

interface UserDetails {
  username: string;
  password: string;
}

function LoginForm() {
  const [user, setLoginUser] = useState<UserDetails>({
    username: '',
    password: '',
  });

  const { setUser } = useContext(UserContext);
  const navigate = useNavigate();
  const [loading, setLoading] = useState<boolean>(false);

  const doSubmit = async () => {
    setLoading(true);
    if (setUser) await auth.login(user.username, user.password, setUser);
    navigate('/Movies');
  };

  // const checkUser = async () => auth.getCurrentUser();
  // if (checkUser()) navigate('');

  const onChangeHandler = (ev: HTMLInputElement) => {
    setLoginUser({
      ...user,
      [ev.name]: ev.value,
    });
  };
  return (
    <div>
      <h1>Login</h1>
      <MDBValidation className="mt-2" id="register-form" onSubmit={doSubmit}>
        <MDBValidationItem feedback="Username is required" invalid>
          <MDBInput
            name="username"
            disabled={loading}
            label="Username"
            type="text"
            className="mb-2"
            value={user.username}
            onChange={({
              currentTarget,
            }: {
              currentTarget: HTMLInputElement;
            }) => onChangeHandler(currentTarget)}
            required
          />
        </MDBValidationItem>
        <MDBValidationItem feedback="Password is required" invalid>
          <MDBInput
            name="password"
            disabled={loading}
            label="Password"
            type="password"
            className="mb-2"
            autoComplete="on"
            value={user.password}
            onChange={({
              currentTarget,
            }: {
              currentTarget: HTMLInputElement;
            }) => onChangeHandler(currentTarget)}
            required
          />
        </MDBValidationItem>
        <MDBBtn
          disabled={loading}
          className="btn btn-primary"
          form="register-form"
          type="submit"
        >
          Submit
        </MDBBtn>
      </MDBValidation>
    </div>
  );
}

export default LoginForm;
