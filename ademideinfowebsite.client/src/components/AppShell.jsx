import { Box, Toolbar } from "@mui/material";
import { Outlet } from "react-router-dom";

import Navbar from "./NavBar";
import SideMenu from "../components/SideMenu";
import Footer from "../components/Footer";

export default function AppShell() {
    return (
        <Box sx={{ display: "flex", minHeight: "100vh" }}>
            {/* Sidebar */}
            <SideMenu />

            {/* Main Content */}
            <Box
                component="main"
                sx={{
                    flexGrow: 1,
                    display: "flex",
                    flexDirection: "column",
                    bgcolor: "#f5f5f5",
                    minHeight: "100vh",
                }}
            >
                <Navbar />

                {/* Prevent content from hiding behind AppBar */}
                <Toolbar />

                <Box
                    sx={{
                        flexGrow: 1,
                        p: 3,
                    }}
                >
                    <Outlet />
                </Box>

                <Footer />
            </Box>
        </Box>
    );
}