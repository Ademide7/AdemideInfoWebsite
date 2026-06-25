import { AppBar, Box, Button, Container, Toolbar, Typography } from '@mui/material';
import { Link, Outlet } from 'react-router-dom';
import { useAuth } from '../context/AuthContext';


export function AppShell() {
    const { user, logout } = useAuth();
    console.log(`[APP SHELL] Current user: ${user}`);
    console.log('[APP SHELL] Rendering navigation with user:', user?.email || 'Not logged in');

    return (
        <Box sx={{ minHeight: '100vh', bgcolor: '#fff', color: '#17202a' }}>
            {/* Navigation bar */}
            <AppBar position="sticky" elevation={1} color="inherit">
                <Toolbar sx={{ gap: 2, flexWrap: 'wrap' }}>
                    {/* App logo/title */}
                    <Typography component={Link} to="/" variant="h6" sx={{ color: 'inherit', textDecoration: 'none', fontWeight: 800 }}>
                        FoodFlow
                    </Typography>

                    {/* Navigation links */}
                    <Button component={Link} to="/home">Home</Button>
                    <Button component={Link} to="/cart">Cart</Button>
                    <Button component={Link} to="/orders">Orders</Button>
                    <Button component={Link} to="/dashboard">Dashboard</Button>

                    {/* Spacer to push auth buttons to the right */}
                    <Box sx={{ flex: 1 }} />

                    {/* User info and logout button (shown when logged in) */}
                    {user ? (
                        <>
                            <Typography variant="body2">{user.fullName}</Typography>
                            <Button onClick={() => {
                                console.log('[APP SHELL] User clicked logout');
                                logout();
                            }}>Logout</Button>
                        </>
                    ) : (
                        // Login/Register buttons (shown when logged out)
                        <>
                            <Button component={Link} to="/login">Login</Button>
                            <Button component={Link} to="/register" variant="contained">Register</Button>
                        </>
                    )}
                </Toolbar>
            </AppBar>

            {/* Main content area - Outlet renders the current page */}
            <Container maxWidth="lg" sx={{ py: 4 }}>
                <Outlet />
            </Container>

            {/* Footer */}
            <Box component="footer" sx={{ borderTop: '1px solid #edf0f2', py: 3, textAlign: 'center' }}>
                <Typography variant="body2" color="text.secondary">
                    Fresh meals, clear tracking, simple dashboards.
                </Typography>
            </Box>
        </Box>
    );
}
export default AppShell;