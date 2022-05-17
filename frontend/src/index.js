import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import HomeView from './views/HomeView';
import DepartmentView from './views/DepartmentView';
import EmployeeView from './views/EmployeeView';
import reportWebVitals from './reportWebVitals';
import {
  BrowserRouter,
  Routes,
  Route,
} from "react-router-dom";

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <React.StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<HomeView />} />
        <Route path="/department" element={<DepartmentView />} />
        <Route path="/employee" element={<EmployeeView />} />
      </Routes>
  </BrowserRouter>
  </React.StrictMode>
);

reportWebVitals();
