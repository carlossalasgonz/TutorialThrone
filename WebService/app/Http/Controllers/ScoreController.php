<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;

class ScoreController extends Controller {

    public function index($page = 1) {
        $scores = Score::with('user')
            ->orderBy('score', 'desc')
            ->paginate(10, ['*'], 'page', $page);

        $response['page'] = $scores->currentPage();
        $response['totalPages'] = $scores->lastPage();
        $response['scores'] = $scores->map( function($item){
            return [
                'id'            =>  $item->id,
                'user'          =>  $item->user->name,
                'score'         =>  $item->score,
                'created_at'    =>  $item->created_at,
            ];
        });

        return response(200, $response);
    }

    public function store(Request $request) {
        $request->validate([
            'device_id' => 'required|string',
            'score'     => 'required|int',
        ]);

        $user = User::where('device_id', $request->device_id)->first();
        if (!$user) {
            $user = User::create([
                'name'      => $request->name ?? 'player_'.rand(1000, 9999),
                'device_id' => $request->device_id,
            ]);
        }

        $score = Score::create([
            'user_id'   =>  $user->id,
            'score'     =>  $request->score,
        ]);

        return response(201, [
            'id'            =>  $score->id,
            'user'          =>  $user->name,
            'score'         =>  $score->score,
            'created_at'    =>  $score->created_at,
        ]);
    }
}
