import { MDBBtn, MDBInput, MDBValidation } from 'mdb-react-ui-kit';
import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import auth from '../services/authService';

interface UserDetails {
  username: string;
  password: string;
}
function LoginForm() {
  const [user, setUser] = useState<UserDetails>({
    username: '',
    password: '',
  });

  const navigate = useNavigate();

  const doSubmit = async () => {
    await auth.login(user.username, user.password);
    navigate('');
  };

  // const checkUser = async () => auth.getCurrentUser();
  // if (checkUser()) navigate('');

  const onChangeHandler = (ev: HTMLInputElement) => {
    setUser({
      ...user,
      [ev.name]: ev.value,
    });
  };
  return (
    <div>
      <h1>Login</h1>
      <MDBValidation className="mt-2" id="register-form" onSubmit={doSubmit}>
        <MDBInput
          name="username"
          label="Username"
          type="text"
          value={user.username}
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
        <MDBBtn className="btn btn-primary" form="register-form" type="submit">
          Submit
        </MDBBtn>
      </MDBValidation>
    </div>
  );
}

export default LoginForm;
