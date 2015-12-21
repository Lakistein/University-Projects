#ifndef RESERVATION_H_INCLUDED
#define RESERVATION_H_INCLUDED


void makeReservationExistingP(struct Passenger *passenger, struct Flight *scheduledFlight, short n, short newPass); // function to make a reservation for existing passenger
void makeReservationNewP(struct Passenger *passenger, struct Flight *scheduledFlight);                              // function to make a reservation for new passenger
void show_reservations(const struct Passenger *passenger, const struct Flight *flight);                             // function to show a reservaton for chosen passenger
short findFreeElement(struct Passenger *passenger, short n);                                                        // function to find a free slot in array for reservation
void menu(void);                                                                                                    // function to print main menu


#endif // RESERVATION_H_INCLUDED
