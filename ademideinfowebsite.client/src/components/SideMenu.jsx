import {
    Drawer,
    List,
    ListItemButton,
    ListItemIcon,
    ListItemText,
    Toolbar,
} from "@mui/material";

import DashboardIcon from "@mui/icons-material/Dashboard";
import PersonIcon from "@mui/icons-material/Person";
import EventIcon from "@mui/icons-material/Event";
import LogoutIcon from "@mui/icons-material/Logout";

import { Link } from "react-router-dom";
import useAuth from "../context/useAuth";

const drawerWidth = 240;

export default function SideMenu() {
    const { logout } = useAuth();

    return (
        <Drawer
            variant="permanent"
            sx={{
                width: drawerWidth,
                flexShrink: 0,
                "& .MuiDrawer-paper": {
                    width: drawerWidth,
                    boxSizing: "border-box",
                },
            }}
        >
            <Toolbar />

            <List>

                <ListItemButton component={Link} to="/dashboard">
                    <ListItemIcon>
                        <DashboardIcon />
                    </ListItemIcon>

                    <ListItemText primary="Dashboard" />
                </ListItemButton>

                <ListItemButton component={Link} to="/profile">
                    <ListItemIcon>
                        <PersonIcon />
                    </ListItemIcon>

                    <ListItemText primary="Profile" />
                </ListItemButton>

                <ListItemButton component={Link} to="/appointments">
                    <ListItemIcon>
                        <EventIcon />
                    </ListItemIcon>

                    <ListItemText primary="Appointments" />
                </ListItemButton>

                <ListItemButton onClick={logout}>
                    <ListItemIcon>
                        <LogoutIcon />
                    </ListItemIcon>

                    <ListItemText primary="Logout" />
                </ListItemButton>

            </List>
        </Drawer>
    );
}