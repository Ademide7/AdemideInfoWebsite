import { Routes, Route } from "react-router-dom";

import AppShell from "../components/AppShell";
import ProtectedRoute from "./ProtectedRoutes";

import Home from "../pages/Home";
import Login from "../pages/Login";
import Register from "../pages/Register";

import Dashboard from "../pages/Dashboard";
import Profile from "../pages/Profile";
import Appointments from "../pages/Appointments";

export default function AppRoutes() {
    return (
        <Routes>

            {/* ---------- Public Routes ---------- */}

            <Route path="/" element={<Home />} />

            <Route
                path="/login"
                element={<Login />}
            />

            <Route
                path="/register"
                element={<Register />}
            />

            {/* ---------- Protected Routes ---------- */}

            <Route element={<ProtectedRoute />}>
                <Route element={<AppShell />}>

                    <Route
                        path="/dashboard"
                        element={<Dashboard />}
                    />

                    <Route
                        path="/profile"
                        element={<Profile />}
                    />

                    <Route
                        path="/appointments"
                        element={<Appointments />}
                    />

                </Route>
            </Route>

            {/* ---------- 404 ---------- */}

            <Route
                path="*"
                element={<h1>404 - Page Not Found</h1>}
            />

        </Routes>
    );
}