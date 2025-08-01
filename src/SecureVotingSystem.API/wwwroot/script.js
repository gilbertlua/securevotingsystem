// ================================================================
// === Secure Voting System Dashboard Application Logic (JavaScript) ===
// ================================================================

document.addEventListener('DOMContentLoaded', () => {

    // --- DOM Elements ---
    const contentContainer = document.getElementById('content-container');
    const navLinks = document.querySelectorAll('.nav-link');
    const modal = document.getElementById('modal');
    const modalTitle = document.getElementById('modal-title');
    const modalBody = document.getElementById('modal-body');
    const closeModalBtn = document.getElementById('close-modal-btn');
    const candidatesLink = document.getElementById('candidates-link');
    const votersLink = document.getElementById('voters-link');
    const electionsLink = document.getElementById('elections-link');

    // --- Configuration & Mock Data ---
    // The base URL for the API. Since the provided API is not a live endpoint,
    // we will use a mock implementation. For a real application, you would
    // change this to your actual API URL.
    const baseUrl = 'http://localhost:5000/api'; // Mock base URL

    // Mock data to simulate API responses.
    let mockCandidates = [
        { id: 1, fullName: 'Alice Johnson', created: '2024-07-25', modified: '2024-07-25' },
        { id: 2, fullName: 'Bob Williams', created: '2024-07-25', modified: '2024-07-25' },
        { id: 3, fullName: 'Charlie Davis', created: '2024-07-26', modified: '2024-07-26' },
    ];
    let mockVotes = [
        { id: 101, voterId: 1, candidateId: 2, voteTimestamp: '2024-07-26T10:00:00Z' },
        { id: 102, voterId: 2, candidateId: 1, voteTimestamp: '2024-07-26T10:05:00Z' },
        { id: 103, voterId: 3, candidateId: 2, voteTimestamp: '2024-07-26T10:10:00Z' },
    ];
    let mockElections = [
        { id: 201, candidateId: 1, activationCode: 'abc1234', timestamp: '2024-07-26T11:00:00Z' },
        { id: 202, candidateId: 3, activationCode: 'def5678', timestamp: '2024-07-26T12:00:00Z' },
    ];

    // --- API Mocking Functions ---
    // These functions simulate the API calls with a delay.
    const mockApi = {
        // Fetches all candidates
        getAllCandidates: () => new Promise(resolve => setTimeout(() => resolve(mockCandidates), 500)),
        // Creates a new candidate
        createCandidate: (candidateDto) => new Promise(resolve => {
            setTimeout(() => {
                const newCandidate = {
                    id: Math.max(...mockCandidates.map(c => c.id)) + 1,
                    fullName: candidateDto.fullName,
                    created: new Date().toISOString().split('T')[0],
                    modified: new Date().toISOString().split('T')[0],
                };
                mockCandidates.push(newCandidate);
                resolve(newCandidate);
            }, 500);
        }),
        // Fetches all votes
        getAllVotes: () => new Promise(resolve => setTimeout(() => resolve(mockVotes), 500)),
        // Creates a new vote/voter
        createVote: (voterDto) => new Promise(resolve => {
            setTimeout(() => {
                const newVote = {
                    id: Math.max(...mockVotes.map(v => v.id)) + 1,
                    voterId: Math.floor(Math.random() * 1000) + 1, // Mock voter ID
                    candidateId: mockCandidates[Math.floor(Math.random() * mockCandidates.length)].id,
                    voteTimestamp: new Date().toISOString(),
                    voterInfo: voterDto // Store the voter info for display
                };
                mockVotes.push(newVote);
                resolve(newVote);
            }, 500);
        }),
        // Fetches all elections
        getAllElections: () => new Promise(resolve => setTimeout(() => resolve(mockElections), 500)),
        // Adds a new election
        addElection: (activationCode, candidateId) => new Promise(resolve => {
            setTimeout(() => {
                const newElection = {
                    id: Math.max(...mockElections.map(e => e.id)) + 1,
                    candidateId: candidateId,
                    activationCode: activationCode,
                    timestamp: new Date().toISOString(),
                };
                mockElections.push(newElection);
                resolve(newElection);
            }, 500);
        })
    };

    // --- UI Rendering Functions ---

    // Renders the Candidates section
    const renderCandidatesView = async () => {
        contentContainer.innerHTML = `
            <div class="flex items-center justify-between mb-6">
                <h2 class="text-3xl font-bold text-gray-800">Candidates</h2>
                <button id="add-candidate-btn" class="btn-primary flex items-center gap-2">
                    <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="lucide lucide-plus"><path d="M5 12h14"/><path d="M12 5v14"/></svg>
                    Add Candidate
                </button>
            </div>
            <div class="bg-white p-6 rounded-2xl shadow-sm">
                <p class="text-gray-500 italic mb-4">Note: The API provided only supports for candidates. Edit and delete actions are simulated for demonstration purposes.</p>
                <div id="candidates-list" class="overflow-x-auto">
                    <p class="text-center text-gray-500 my-8">Loading candidates...</p>
                </div>
            </div>
        `;
        const addCandidateBtn = document.getElementById('add-candidate-btn');
        addCandidateBtn.addEventListener('click', showAddCandidateModal);
        await refreshCandidatesList();
    };

    // Renders the list of candidates inside the view
    const refreshCandidatesList = async () => {
        const candidatesList = document.getElementById('candidates-list');
        candidatesList.innerHTML = `<p class="text-center text-gray-500 my-8">Loading candidates...</p>`;
        try {
            const candidates = await mockApi.getAllCandidates();
            if (candidates.length === 0) {
                candidatesList.innerHTML = `<p class="text-center text-gray-500 my-8">No candidates found.</p>`;
                return;
            }
            candidatesList.innerHTML = `
                <table class="data-table">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Full Name</th>
                            <th>Created</th>
                            <th>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        ${candidates.map(c => `
                            <tr class="hover:bg-gray-50 transition-colors duration-150">
                                <td class="font-mono">${c.id}</td>
                                <td>${c.fullName}</td>
                                <td class="text-gray-500">${new Date(c.created).toLocaleDateString()}</td>
                                <td>
                                    <button class="text-blue-500 hover:text-blue-700 transition-colors duration-200">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="lucide lucide-pencil"><path d="M17 3a2.85 2.83 0 1 1 4 4L7.5 20.5 2 22l1.5-5.5Z"/><path d="m15 5 4 4"/></svg>
                                    </button>
                                    <button class="text-red-500 hover:text-red-700 transition-colors duration-200 ml-2">
                                        <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="lucide lucide-trash"><path d="M3 6h18"/><path d="M19 6v14c0 1-1 2-2 2H7c-1 0-2-1-2-2V6"/><path d="M8 6V4c0-1 1-2 2-2h4c1 0 2 1 2 2v2"/></svg>
                                    </button>
                                </td>
                            </tr>
                        `).join('')}
                    </tbody>
                </table>
            `;
            lucide.createIcons();
        } catch (error) {
            candidatesList.innerHTML = `<p class="text-center text-red-500 my-8">Failed to load candidates.</p>`;
        }
    };

    // Renders the form to add a new candidate
    const showAddCandidateModal = () => {
        modalTitle.textContent = 'Add New Candidate';
        modalBody.innerHTML = `
            <form id="add-candidate-form" class="space-y-4">
                <div>
                    <label for="fullName" class="block text-sm font-medium text-gray-700 mb-2">Full Name</label>
                    <input type="text" id="fullName" name="fullName" required class="w-full">
                </div>
                <div class="flex justify-end gap-4 mt-6">
                    <button type="button" id="cancel-add-candidate" class="btn-secondary">Cancel</button>
                    <button type="submit" id="submit-add-candidate" class="btn-primary">
                        <span id="loading-spinner" class="hidden">
                            <svg class="animate-spin h-5 w-5 text-white" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                                <circle class="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" stroke-width="4"></circle>
                                <path class="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
                            </svg>
                        </span>
                        <span id="submit-text">Create Candidate</span>
                    </button>
                </div>
            </form>
            <div id="status-message" class="mt-4 text-center"></div>
        `;
        modal.classList.remove('hidden');
        modal.classList.add('flex');

        const form = document.getElementById('add-candidate-form');
        form.addEventListener('submit', handleAddCandidate);

        document.getElementById('cancel-add-candidate').addEventListener('click', closeModal);
    };

    // Handles the form submission for creating a new candidate
    const handleAddCandidate = async (e) => {
        e.preventDefault();
        const form = e.target;
        const fullName = form.querySelector('#fullName').value;
        const statusMessage = document.getElementById('status-message');
        const submitBtn = document.getElementById('submit-add-candidate');
        const submitText = document.getElementById('submit-text');
        const loadingSpinner = document.getElementById('loading-spinner');

        submitBtn.disabled = true;
        submitBtn.classList.add('bg-blue-400');
        submitText.classList.add('hidden');
        loadingSpinner.classList.remove('hidden');
        statusMessage.textContent = '';

        try {
            const candidateDto = { fullName };
            const newCandidate = await mockApi.createCandidate(candidateDto);
            statusMessage.innerHTML = `<p class="text-green-600 font-semibold">Candidate "${newCandidate.fullName}" created successfully!</p>`;
            form.reset();
            setTimeout(() => {
                closeModal();
                refreshCandidatesList();
            }, 1000); // Close modal and refresh after 1 second
        } catch (error) {
            statusMessage.innerHTML = `<p class="text-red-500">Failed to create candidate. Please try again.</p>`;
        } finally {
            submitBtn.disabled = false;
            submitBtn.classList.remove('bg-blue-400');
            submitText.classList.remove('hidden');
            loadingSpinner.classList.add('hidden');
        }
    };


    // Renders the Votes section
    const renderVotesView = async () => {
        contentContainer.innerHTML = `
            <div class="flex items-center justify-between mb-6">
                <h2 class="text-3xl font-bold text-gray-800">Votes</h2>
                <button id="add-vote-btn" class="btn-primary flex items-center gap-2">
                    <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="lucide lucide-plus"><path d="M5 12h14"/><path d="M12 5v14"/></svg>
                    Add Vote
                </button>
            </div>
            <div class="bg-white p-6 rounded-2xl shadow-sm">
                <p class="text-gray-500 italic mb-4">Note: The API provides an endpoint to get all votes and create a new vote (which also registers the voter). The "Voter" section title in the API refers to a voter's action (voting) rather than the voter entity itself.</p>
                <div id="votes-list" class="overflow-x-auto">
                    <p class="text-center text-gray-500 my-8">Loading votes...</p>
                </div>
            </div>
        `;
        const addVoteBtn = document.getElementById('add-vote-btn');
        addVoteBtn.addEventListener('click', showAddVoteModal);
        await refreshVotesList();
    };

    // Renders the list of votes
    const refreshVotesList = async () => {
        const votesList = document.getElementById('votes-list');
        votesList.innerHTML = `<p class="text-center text-gray-500 my-8">Loading votes...</p>`;
        try {
            const votes = await mockApi.getAllVotes();
            if (votes.length === 0) {
                votesList.innerHTML = `<p class="text-center text-gray-500 my-8">No votes cast yet.</p>`;
                return;
            }
            votesList.innerHTML = `
                <table class="data-table">
                    <thead>
                        <tr>
                            <th>Vote ID</th>
                            <th>Voter ID</th>
                            <th>Candidate ID</th>
                            <th>Timestamp</th>
                        </tr>
                    </thead>
                    <tbody>
                        ${votes.map(v => `
                            <tr class="hover:bg-gray-50 transition-colors duration-150">
                                <td class="font-mono">${v.id}</td>
                                <td class="font-mono">${v.voterId}</td>
                                <td class="font-mono">${v.candidateId}</td>
                                <td class="text-gray-500">${new Date(v.voteTimestamp).toLocaleString()}</td>
                            </tr>
                        `).join('')}
                    </tbody>
                </table>
            `;
        } catch (error) {
            votesList.innerHTML = `<p class="text-center text-red-500 my-8">Failed to load votes.</p>`;
        }
    };

    // Renders the form to create a new vote
    const showAddVoteModal = () => {
        modalTitle.textContent = 'Add New Vote (and Register Voter)';
        modalBody.innerHTML = `
            <form id="add-vote-form" class="space-y-4">
                <div>
                    <label for="voterFullName" class="block text-sm font-medium text-gray-700 mb-2">Voter Full Name</label>
                    <input type="text" id="voterFullName" name="voterFullName" required class="w-full">
                </div>
                <div>
                    <label for="voterPhoneNumber" class="block text-sm font-medium text-gray-700 mb-2">Voter Phone Number</label>
                    <input type="text" id="voterPhoneNumber" name="voterPhoneNumber" required class="w-full">
                </div>
                <div class="flex justify-end gap-4 mt-6">
                    <button type="button" id="cancel-add-vote" class="btn-secondary">Cancel</button>
                    <button type="submit" id="submit-add-vote" class="btn-primary">
                        <span id="loading-spinner" class="hidden">
                            <svg class="animate-spin h-5 w-5 text-white" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                                <circle class="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" stroke-width="4"></circle>
                                <path class="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
                            </svg>
                        </span>
                        <span id="submit-text">Create Vote</span>
                    </button>
                </div>
            </form>
            <div id="status-message" class="mt-4 text-center"></div>
        `;
        modal.classList.remove('hidden');
        modal.classList.add('flex');

        const form = document.getElementById('add-vote-form');
        form.addEventListener('submit', handleAddVote);

        document.getElementById('cancel-add-vote').addEventListener('click', closeModal);
    };

    // Handles the form submission for creating a new vote
    const handleAddVote = async (e) => {
        e.preventDefault();
        const form = e.target;
        const voterFullName = form.querySelector('#voterFullName').value;
        const voterPhoneNumber = form.querySelector('#voterPhoneNumber').value;
        const statusMessage = document.getElementById('status-message');
        const submitBtn = document.getElementById('submit-add-vote');
        const submitText = document.getElementById('submit-text');
        const loadingSpinner = document.getElementById('loading-spinner');

        submitBtn.disabled = true;
        submitBtn.classList.add('bg-blue-400');
        submitText.classList.add('hidden');
        loadingSpinner.classList.remove('hidden');
        statusMessage.textContent = '';

        try {
            const voterDto = { fullName: voterFullName, phoneNumber: voterPhoneNumber };
            const newVote = await mockApi.createVote(voterDto);
            statusMessage.innerHTML = `<p class="text-green-600 font-semibold">Vote #${newVote.id} created successfully!</p>`;
            form.reset();
            setTimeout(() => {
                closeModal();
                refreshVotesList();
            }, 1000);
        } catch (error) {
            statusMessage.innerHTML = `<p class="text-red-500">Failed to create vote. Please try again.</p>`;
        } finally {
            submitBtn.disabled = false;
            submitBtn.classList.remove('bg-blue-400');
            submitText.classList.remove('hidden');
            loadingSpinner.classList.add('hidden');
        }
    };


    // Renders the Elections section
    const renderElectionsView = async () => {
        contentContainer.innerHTML = `
            <div class="flex items-center justify-between mb-6">
                <h2 class="text-3xl font-bold text-gray-800">Elections</h2>
                <button id="add-election-btn" class="btn-primary flex items-center gap-2">
                    <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2" stroke-linecap="round" stroke-linejoin="round" class="lucide lucide-plus"><path d="M5 12h14"/><path d="M12 5v14"/></svg>
                    Add Election
                </button>
            </div>
            <div class="bg-white p-6 rounded-2xl shadow-sm">
                <p class="text-gray-500 italic mb-4">Note: An election in this context links a candidate to an activation code (token) to track a vote. This is based on the API's 'add-election' endpoint.</p>
                <div id="elections-list" class="overflow-x-auto">
                    <p class="text-center text-gray-500 my-8">Loading elections...</p>
                </div>
            </div>
        `;
        const addElectionBtn = document.getElementById('add-election-btn');
        addElectionBtn.addEventListener('click', showAddElectionModal);
        await refreshElectionsList();
    };

    // Renders the list of elections
    const refreshElectionsList = async () => {
        const electionsList = document.getElementById('elections-list');
        electionsList.innerHTML = `<p class="text-center text-gray-500 my-8">Loading elections...</p>`;
        try {
            const elections = await mockApi.getAllElections();
            if (elections.length === 0) {
                electionsList.innerHTML = `<p class="text-center text-gray-500 my-8">No elections found.</p>`;
                return;
            }
            electionsList.innerHTML = `
                <table class="data-table">
                    <thead>
                        <tr>
                            <th>Election ID</th>
                            <th>Candidate ID</th>
                            <th>Activation Code</th>
                            <th>Timestamp</th>
                        </tr>
                    </thead>
                    <tbody>
                        ${elections.map(e => `
                            <tr class="hover:bg-gray-50 transition-colors duration-150">
                                <td class="font-mono">${e.id}</td>
                                <td class="font-mono">${e.candidateId}</td>
                                <td class="font-mono">${e.activationCode}</td>
                                <td class="text-gray-500">${new Date(e.timestamp).toLocaleString()}</td>
                            </tr>
                        `).join('')}
                    </tbody>
                </table>
            `;
        } catch (error) {
            electionsList.innerHTML = `<p class="text-center text-red-500 my-8">Failed to load elections.</p>`;
        }
    };

    // Renders the form to add a new election
    const showAddElectionModal = async () => {
        modalTitle.textContent = 'Add New Election';
        const candidates = await mockApi.getAllCandidates();
        modalBody.innerHTML = `
            <form id="add-election-form" class="space-y-4">
                <div>
                    <label for="activationCode" class="block text-sm font-medium text-gray-700 mb-2">Activation Code / Voter Token</label>
                    <input type="text" id="activationCode" name="activationCode" required class="w-full">
                </div>
                <div>
                    <label for="candidateId" class="block text-sm font-medium text-gray-700 mb-2">Candidate</label>
                    <select id="candidateId" name="candidateId" required class="w-full">
                        <option value="" disabled selected>Select a candidate</option>
                        ${candidates.map(c => `<option value="${c.id}">${c.fullName} (ID: ${c.id})</option>`).join('')}
                    </select>
                </div>
                <div class="flex justify-end gap-4 mt-6">
                    <button type="button" id="cancel-add-election" class="btn-secondary">Cancel</button>
                    <button type="submit" id="submit-add-election" class="btn-primary">
                        <span id="loading-spinner" class="hidden">
                            <svg class="animate-spin h-5 w-5 text-white" xmlns="http://www.w3.org/2000/svg" fill="none" viewBox="0 0 24 24">
                                <circle class="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" stroke-width="4"></circle>
                                <path class="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"></path>
                            </svg>
                        </span>
                        <span id="submit-text">Add Election</span>
                    </button>
                </div>
            </form>
            <div id="status-message" class="mt-4 text-center"></div>
        `;
        modal.classList.remove('hidden');
        modal.classList.add('flex');

        const form = document.getElementById('add-election-form');
        form.addEventListener('submit', handleAddElection);

        document.getElementById('cancel-add-election').addEventListener('click', closeModal);
    };

    // Handles the form submission for adding a new election
    const handleAddElection = async (e) => {
        e.preventDefault();
        const form = e.target;
        const activationCode = form.querySelector('#activationCode').value;
        const candidateId = parseInt(form.querySelector('#candidateId').value, 10);
        const statusMessage = document.getElementById('status-message');
        const submitBtn = document.getElementById('submit-add-election');
        const submitText = document.getElementById('submit-text');
        const loadingSpinner = document.getElementById('loading-spinner');

        submitBtn.disabled = true;
        submitBtn.classList.add('bg-blue-400');
        submitText.classList.add('hidden');
        loadingSpinner.classList.remove('hidden');
        statusMessage.textContent = '';

        try {
            await mockApi.addElection(activationCode, candidateId);
            statusMessage.innerHTML = `<p class="text-green-600 font-semibold">Election added successfully!</p>`;
            form.reset();
            setTimeout(() => {
                closeModal();
                refreshElectionsList();
            }, 1000);
        } catch (error) {
            statusMessage.innerHTML = `<p class="text-red-500">Failed to add election. Please try again.</p>`;
        } finally {
            submitBtn.disabled = false;
            submitBtn.classList.remove('bg-blue-400');
            submitText.classList.remove('hidden');
            loadingSpinner.classList.add('hidden');
        }
    };

    // --- Modal Management ---
    const showModal = () => {
        modal.classList.remove('hidden');
        modal.classList.add('flex');
    };

    const closeModal = () => {
        modal.classList.add('hidden');
        modal.classList.remove('flex');
        modalBody.innerHTML = '';
        modalTitle.textContent = '';
    };

    // --- Event Listeners ---
    closeModalBtn.addEventListener('click', closeModal);
    modal.addEventListener('click', (e) => {
        if (e.target === modal) {
            closeModal();
        }
    });

    candidatesLink.addEventListener('click', (e) => {
        e.preventDefault();
        setActiveLink(candidatesLink);
        renderCandidatesView();
    });

    votersLink.addEventListener('click', (e) => {
        e.preventDefault();
        setActiveLink(votersLink);
        renderVotesView();
    });

    electionsLink.addEventListener('click', (e) => {
        e.preventDefault();
        setActiveLink(electionsLink);
        renderElectionsView();
    });

    // Function to set the active state on a sidebar link
    const setActiveLink = (activeLink) => {
        navLinks.forEach(link => {
            link.classList.remove('active');
        });
        activeLink.classList.add('active');
    };

    // --- Initial View ---
    // Load the candidates view by default when the page loads
    candidatesLink.click();
});
