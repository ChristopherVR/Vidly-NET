import React, { lazy, Suspense, useEffect, useMemo, useState } from 'react';
import { Route, Routes } from 'react-router-dom';
import { ToastContainer } from 'react-toastify';
import auth from './services/authService';
import 'react-toastify/dist/ReactToastify.css';
import './App.scss';
import { User } from './interfaces/user';
import UserContext from './context/userContext';
import Loading from './components/Loading';
import Home from './components/Home';
import NotFound from './components/NotFound';

const Movies = lazy(() => import('./components/Movies'));

const MovieForm = lazy(() => import('./components/MovieForm'));

const Customers = lazy(() => import('./components/Customers'));

const Rentals = lazy(() => import('./components/Rentals'));

const NavBar = lazy(() => import('./components/NavBar'));

const LoginForm = lazy(() => import('./components/LoginForm'));

const RegisterForm = lazy(() => import('./components/RegisterForm'));

const Logout = lazy(() => import('./components/Logout'));

const Profile = lazy(() => import('./components/Profile'));

function App() {
  const [user, setUser] = useState<User>();
  useEffect(() => {
    (async () => {
      const getUser = async () => {
        const response = await auth.getCurrentUser();
        if (response) {
          setUser(response);
        }
      };
      await getUser();
    })();
  }, []);

  const userContextProvider = useMemo(
    () => ({
      user,
      setUser,
    }),
    [user],
  );

  return (
    <Suspense fallback={<Loading />}>
      <UserContext.Provider value={userContextProvider}>
        <ToastContainer
          position="bottom-left"
          autoClose={5000}
          newestOnTop
          closeOnClick
          pauseOnHover
        />
        <NavBar />
        <main className="container p-5 mx-auto d-flex justify-content-center">
          <Routes>
            <Route path="" element={<Home />} />
            <Route path="/register" element={<RegisterForm />} />
            <Route path="/login" element={<LoginForm />} />
            <Route path="/logout" element={<Logout />} />
            <Route path="/movies" element={<Movies />} />
            <Route path="/movies/:id" element={<MovieForm />} />
            <Route path="/customers" element={<Customers />} />
            <Route path="/rentals" element={<Rentals />} />
            <Route path="/profile" element={<Profile />} />
            <Route path="*" element={<NotFound />} />
          </Routes>
        </main>
      </UserContext.Provider>
    </Suspense>
  );
}

export default App;
