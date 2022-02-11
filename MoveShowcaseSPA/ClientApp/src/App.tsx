import React, { Component, useEffect, useState } from 'react';
import { Route, Redirect, Switch, Routes } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import axios from 'axios';
import Movies from './components/movies';
import MovieForm from './components/movieForm';
import Customers from './components/customers';
import Rentals from './components/rentals';
import NotFound from './components/NotFound';
import NavBar from './components/navBar';
import LoginForm from './components/loginForm';
import RegisterForm from './components/registerForm';
import Logout from './components/logout';
import ProtectedRoute from './components/common/protectedRoute';
import auth from './services/authService';
import 'react-toastify/dist/ReactToastify.css';
import './App.css';
import { User } from './interfaces/user';
import UserContext from './context/userContext';

function App() {
  const [user, setUser] = useState<User>();
  useEffect(() => {
    (async () => {
      const updateUser = ({ user: userResponse }: { user: User }) => {
        setUser(userResponse);
      };

      const getUser = async () => {
        const response = await auth.getCurrentUser();
        updateUser(response.data);
      };
      await getUser();
    })();
  }, []);

  return (
    <UserContext.Provider value={user}>
      <ToastContainer />
      <NavBar user={user} />
      <main className="container">
        <Routes>
          <Route path="/register" element={RegisterForm} />
          <Route path="/login" element={LoginForm} />
          <Route path="/logout" element={Logout} />
          <ProtectedRoute
            path="/movies/:id"
            component={MovieForm}
            render={undefined}
          />
          <Route path="/movies" element={<Movies user={user} />} />
          <Route path="/customers" element={Customers} />
          <Route path="/rentals" element={Rentals} />
          <Route path="/not-found" element={NotFound} />
          <Redirect from="/" exact to="/movies" />
          <Redirect to="/not-found" />
        </Routes>
      </main>
    </UserContext.Provider>
  );
}

export default App;
