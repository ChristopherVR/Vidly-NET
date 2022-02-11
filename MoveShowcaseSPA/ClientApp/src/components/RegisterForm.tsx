import React, { useState } from 'react';
import Joi from 'joi-browser';
import Form from './common/formValidation';
import * as userService from '../services/userService';
import auth from '../services/authService';
import { User } from '../interfaces/user';

function RegisterForm() {
  const [user, setUser] = useState<User>();

  const schema = {
    username: Joi.string().required().email().label('Username'),
    password: Joi.string().required().min(5).label('Password'),
    name: Joi.string().required().label('Name'),
  };

  const doSubmit = async () => {
    try {
      await userService.register(user);
      // TODO: set context user here.
      window.location = '/';
    } catch (ex) {
      console.log(ex);
    }
  };
  return (
    <div>
      <h1>Register</h1>
      <form onSubmit={handleSubmit}>
        {renderInput('username', 'Username')}
        {renderInput('password', 'Password', 'password')}
        {renderInput('name', 'Name')}
        {renderButton('Register')}
      </form>
    </div>
  );
}

export default RegisterForm;
