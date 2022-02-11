import React, { useState } from 'react';
import { Redirect } from 'react-router-dom';
import Joi from 'joi-browser';
import Form from './common/formValidation';
import auth from '../services/authService';

interface UserDetails {
  username: string;
  password: string;
}
function LoginForm() {
  const [user, setUser] = useState<UserDetails>();

  const schema = {
    username: Joi.string().required().label('Username'),
    password: Joi.string().required().label('Password'),
  };

  const doSubmit = async () => {
    try {
      await auth.login(user.username, user.password);

      const { state } = location;
      window.location = state ? state.from.pathname : '/';
    } catch (ex) {
      console.log(ex);
    }
  };

  if (await auth.getCurrentUser()) return <Redirect to="/" />;

  return (
    <div>
      <h1>Login</h1>
      <form onSubmit={handleSubmit}>
        {renderInput('username', 'Username')}
        {renderInput('password', 'Password', 'password')}
        {renderButton('Login')}
      </form>
    </div>
  );
}

export default LoginForm;
