import React, { useEffect, useMemo, useState } from 'react';
import { Route, Routes } from 'react-router-dom';
import { toast, ToastContainer } from 'react-toastify';
import Movies from './components/Movies';
import Customers from './components/Customers';
import Rentals from './components/Rentals';
import NotFound from './components/NotFound';
import NavBar from './components/NavBar';
import LoginForm from './components/LoginForm';
import RegisterForm from './components/RegisterForm';
import Logout from './components/Logout';
import auth from './services/authService';
import 'react-toastify/dist/ReactToastify.css';
import './App.scss';
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
        if (!response) {
          toast.error('🦄 Wow so easy!', {
            closeOnClick: true,
            pauseOnHover: true,
            draggable: false,
          });
        } else {
          updateUser(response?.data);
        }
      };
      await getUser();
    })();
  }, []);

  const userContextProvider = useMemo(
    () => ({
      user: user ?? ({} as User),
      setUser,
    }),
    [user],
  );

  if (!user) return null;

  return (
    <UserContext.Provider value={userContextProvider}>
      <ToastContainer
        position="bottom-left"
        autoClose={5000}
        hideProgressBar={false}
        newestOnTop
        closeOnClick
        rtl={false}
        pauseOnFocusLoss={false}
        draggable={false}
        pauseOnHover
      />
      <NavBar />
      <main className="container">
        <Routes>
          <Route path="/register" element={<RegisterForm />} />
          <Route path="/login" element={<LoginForm />} />
          <Route path="/logout" element={<Logout />} />
          <Route path="/movies" element={<Movies />} />
          <Route path="/customers" element={<Customers />} />
          <Route path="/rentals" element={<Rentals />} />
          <Route path="*" element={<NotFound />} />
        </Routes>
      </main>
    </UserContext.Provider>
  );
}

export default App;
