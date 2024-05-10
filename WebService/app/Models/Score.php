<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Score extends Model {
    use HasFactory;

    protected $table = 'scores';
    public $primaryKey = 'id';
    public $fillable = ['user_id', 'score'];
    public $timestamps = true;

    public $casts = [
        'id'            =>  'int',
        'user_id'       =>  'int',
        'score'         =>  'int',
        'created_at'    =>  'datetime',
        'updated_at'    =>  'datetime',
    ];

    public function user() {
        return $this->belongsTo(User::class);
    }
}
