import { Routes, Route } from "react-router-dom";

import AppShell from "../layouts/AppShell";
import ProtectedRoute from "./ProtectedRoute";
import Dashboard from "../pages/Dashboard";
import Profile from "../pages/Profile";
import Appointments from "../pages/Appointments";

<Routes>
    <Route element={<ProtectedRoute />}>
        <Route element={<AppShell />}>
            <Route path="/dashboard" element={<Dashboard />} />
            <Route path="/profile" element={<Profile />} />
            <Route path="/appointments" element={<Appointments />} />
        </Route>
    </Route>
</Routes>